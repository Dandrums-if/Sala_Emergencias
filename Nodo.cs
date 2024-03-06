
namespace hospital{

    public class Nodo{

        public Nodo? Siguiente;
        public Paciente Paciente; 

        

        public Nodo(Paciente paciente){
            this.Paciente = paciente;
            this.Siguiente = null;
        }


        public Paciente GetPaciente(){
            return this.Paciente;
        }
    }
}