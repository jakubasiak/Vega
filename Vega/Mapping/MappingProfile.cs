using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Controllers.Resources;
using Vega.Models;

namespace Vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Email = v.ContactEmail, Name = v.ContactName, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            //API Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr,v) => {
                    //remove unselected features

                    //var unselectedFeatures = new List<VehicleFeature>();
                    //foreach (var f in v.Features)
                    //{
                    //    if (!vr.Features.Contains(f.FeatureId))
                    //        unselectedFeatures.Add(f);
                    //}
                    //foreach (var f in unselectedFeatures)
                    //{
                    //    v.Features.Remove(f);
                    //}
                    var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));
                    foreach (var f in removedFeatures.ToList())
                    {
                        v.Features.Remove(f);
                    }

                    //Add selected features

                    //foreach (var id in vr.Features)
                    //{
                    //    if (!v.Features.Any(f => f.FeatureId == id))
                    //        v.Features.Add(new VehicleFeature { FeatureId = id });
                    //}

                    var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id});
                    foreach (var f in addedFeatures.ToList())
                    {
                        v.Features.Add(f);
                    }
                });
        }
    }
}
