
using System.Data;

namespace hospital{
    public class Paciente{

        //Razones de ingreso
        private static readonly string[] prioridades = {"Problema leve","Parto","Problema respiratorio","Infarto","Accidente aparatoso"};

        //Escala de prioridad
        private readonly int prioridad; 

        //Edad 0 ó 5
        private readonly int edad;

        //Esta vivo o esta muerto
        private bool estado;

        //Razon por la que vino
        private readonly string? razon;


        //Es niño o adulto
        private string? rangoEdad;



        
        //Tiempo que ha pasado en la cola, el cual determina si morira o si se ira del lugar
        private int tiempo;


        public Paciente(int prior,int edad) {

            this.edad = edad;

            this.prioridad = prior;
            
            this.estado = true;

            this.tiempo = 0;


            this.razon = prioridades[prior];


            if(edad==0) rangoEdad = "niño";
            else if(edad==5) rangoEdad = "adulto";

        }

       

        public string GetRazon(){
            return razon!;
        }
        public int GetEdad(){
            return this.edad;
        }

        public string GetRangoEdad(){
            return rangoEdad!;
        }


        /*
        public void SetEstado(bool estado){
            this.estado = estado;
        }
        


        public bool GetEstado(){
            return this.estado;
        }
*/

        //Aumenta el tiempo transcurrido para el paciente
        public void Clock(){
            this.tiempo++;
        }

        public int GetTiempo(){
            return this.tiempo;
        }

        public int GetPrior(){
            return prioridad;
        }

    }
}