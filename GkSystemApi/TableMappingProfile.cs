using AutoMapper;
using gk_system_api.Entities;
using gk_system_api.Models;
using gk_system_api.Utils;

namespace gk_system_api
{
    public class TableMappingProfile : Profile
    {
        public TableMappingProfile()
        {
            CreateMap<Realisation, RealisationViewModel>();
            CreateMap<RealisationImage, RealisationImageViewModel>();

            CreateMap<Offer, OfferViewModel>();
            CreateMap<OfferPlan, OfferPlanViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(o => o.ImageByte.BytesToBase64(o.ImageByteType) ?? o.Image));

            CreateMap<OfferParams, OfferParamsViewModel>();
            CreateMap<OfferVisualisations, OfferVisualisationsViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(o => o.ImageByte.BytesToBase64(o.ImageByteType) ?? o.Image));
            CreateMap<OfferPlanParams, OfferPlanParamsViewModel>();

            CreateMap<ReferenceViewModel, Reference>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Image, opt => opt.MapFrom(o => o.Image == null ? "" : o.Image.ModifyIfBase64()))
                .ForMember(x => x.ImageByte, opt => opt.MapFrom(o => o.Image.Base64ToByte()))
                .ForMember(x => x.ImageByteType, opt => opt.MapFrom(o => o.Image == null ? "" : o.Image.GetImageType()));

            CreateMap<CarouselConfigViewModel, CarouselConfig>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Image, opt => opt.MapFrom(o => o.Image.ModifyIfBase64()))
                .ForMember(x => x.ByteImage, opt => opt.MapFrom(o => o.Image.Base64ToByte()))
                .ForMember(x => x.ByteImageType, opt => opt.MapFrom(o => o.Image.GetImageType()));

            CreateMap<CarouselConfig, CarouselConfigViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(o => o.ByteImage.BytesToBase64(o.ByteImageType) ?? o.Image));


            CreateMap<RealisationViewModel, Realisation>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<RealisationImageViewModel, RealisationImage>()
                .ForMember(x => x.RealisationImageId, opt => opt.Ignore())
                .ForMember(x => x.ImageSrc, opt => opt.MapFrom(o => o.ImageSrc.ModifyIfBase64()))
                .ForMember(x => x.ImageByte, opt => opt.MapFrom(o => o.ImageSrc.Base64ToByte()))
                .ForMember(x => x.ImageByteType, opt => opt.MapFrom(o => o.ImageSrc.GetImageType()));

            CreateMap<RealisationImage, RealisationImageViewModel>()
                .ForMember(x => x.ImageSrc, opt => opt.MapFrom(o => o.ImageByte.BytesToBase64(o.ImageByteType) ?? o.ImageSrc));

            CreateMap<OfferViewModel, Offer>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<OfferPlanViewModel, OfferPlan>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Image, opt => opt.MapFrom(o => o.Image.ModifyIfBase64()))
                .ForMember(x => x.ImageByte, opt => opt.MapFrom(o => o.Image.Base64ToByte()))
                .ForMember(x => x.ImageByteType, opt => opt.MapFrom(o => o.Image.GetImageType()));

            CreateMap<OfferPlanParamsViewModel, OfferPlanParams>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<OfferVisualisationsViewModel, OfferVisualisations>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Image, opt => opt.MapFrom(o => o.Image.ModifyIfBase64()))
                .ForMember(x => x.ImageByte, opt => opt.MapFrom(o => o.Image.Base64ToByte()))
                .ForMember(x => x.ImageByteType, opt => opt.MapFrom(o => o.Image.GetImageType()));

            CreateMap<OfferParamsViewModel, OfferParams>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<GeneralConfigViewModel, GeneralConfig>()
                .ForMember(x => x.Value, opt => opt.MapFrom(o => o.Value.ModifyIfBase64()))
                .ForMember(x => x.Image, opt => opt.MapFrom(o => o.Value.Base64ToByte()))
                .ForMember(x => x.ImageType, opt => opt.MapFrom(o => o.Value.GetImageType()));

            CreateMap<GeneralConfig, GeneralConfigViewModel>()
                .ForMember(x => x.Value, opt => opt.MapFrom(o => o.Image.BytesToBase64(o.ImageType) ?? o.Value));

            CreateMap<Reference, ReferenceViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(o => o.ImageByte.BytesToBase64(o.ImageByteType) ?? o.Image));

            CreateMap<VisitorViewModel, Visitor>();
            CreateMap<LocalisationViewModel, Localisation>()
                .ForMember(x => x.CountryName, opt => opt.MapFrom(o => o.country_name))
                .ForMember(x => x.CountryCode, opt => opt.MapFrom(o => o.country_code));
        }
    }
}
