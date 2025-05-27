using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IPaymentMethodsAplicacion
    {
        void Configurar(string StringConexion);
        List<PaymentMethods> PorActivo(PaymentMethods? entidad);
        List<PaymentMethods> Listar();
        PaymentMethods? Guardar(PaymentMethods? entidad);
        PaymentMethods? Modificar(PaymentMethods? entidad);
        PaymentMethods? Borrar(PaymentMethods? entidad);


    }
}