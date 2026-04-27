using Mapster;
using PaymentService.Application.DTOs;
using PaymentService.Domain.Entities;

namespace PaymentService.Application.Mappings
{
    public class PaymentServiceMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PaymentDetail, GetPaymentResponse>();
            config.NewConfig<CreatePaymentRequest, PaymentDetail>();
            config.NewConfig<UpdatePaymentRequest, PaymentDetail>();
        }
    }
}
