using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ISalesAplicacion
    {
        void Configurar(string StringConexion);
        List<Sales> PorReferencia(Sales? entidad);
        List<Sales> Listar();
        Sales? Guardar(Sales? entidad);
        Sales? Modificar(Sales? entidad);
        Sales? Borrar(Sales? entidad);


    }
}