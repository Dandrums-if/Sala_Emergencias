
namespace hospital{

    public class Medico{
        //Paciente que está atendiendo en el momento
        private Paciente? paciente;
        public Medico()
        {
            this.paciente = null;

        }

        //Le asigna un paciente
        public void SetPaciente(Paciente p){
            this.paciente = p;
        }

        //Retorna el paciente que está atendiento
        public Paciente? GetPaciente(){
            return this.paciente;
        }

        public bool Ocupado(){
            return this.paciente!=null;
        }

    }
}