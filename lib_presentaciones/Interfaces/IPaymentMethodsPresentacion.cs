using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IPaymentMethodsPresentacion
    {
        Task<List<PaymentMethods>> PorActivo(PaymentMethods? entidad);
        Task<List<PaymentMethods>> Listar();
        Task<PaymentMethods?> Guardar(PaymentMethods? entidad);
        Task<PaymentMethods?> Modificar(PaymentMethods? entidad);
        Task<PaymentMethods?> Borrar(PaymentMethods? entidad);


    }
}