using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Implementaciones
{
    public class AuditoriaAplicacion : IAuditoriaAplicacion
    {
        private IConexion? IConexion = null;


        public AuditoriaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Auditoria> Listar()
        {
            return this.IConexion!.Auditoria!.ToList();
        }
    }
}
