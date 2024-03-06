
using System.Security.Cryptography;
namespace hospital{


    public class Persona{

        
        private string nombre;
        private string cedula;
        private int edad;


        public Persona(string nombre, int edad, string cedula){
            this.nombre = nombre;
            this.edad = edad;
            this.cedula = cedula;
        }

        public string getNombre(){
            return this.nombre;
        }

        public string getCedula(){
            return this.cedula;
        }

        public int getEdad(){
            return this.edad;
        }
    }
}