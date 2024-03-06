
using System.Runtime.InteropServices;

namespace hospital{

    public class Cola{

        //Tamaño máximo de cola
        private static int max = 15;

        //Extremos de la cola, pueden ser null
        private Nodo? primero, ultimo;


        //Cantidad de elementos en cola
        private int cantidad;


        //Constructor
        public Cola(){
            cantidad = 0;
            primero = null;
            ultimo = null;
        }

        //Inserta un elemento
        public bool Insertar(Nodo nodo){

            bool flag = false;

            if(cantidad<max){
                if(Vacia()){
                    primero = nodo;
                    ultimo = nodo;
                }else{
                    ultimo!.Siguiente = nodo;
                    ultimo = nodo;
                    
                }
                cantidad++;
                flag = true;
            }


            //false si está llena, true si aún hay espacio
            return flag;            
        }


        //Remueve un elemento de la cola
        public Nodo? Remover(){
            Nodo? aux;
            if(Vacia()){
                aux = null;
            }else{
                aux = primero;
                primero = primero!.Siguiente;
                cantidad--;
            }

            return aux;
        }

        public void Reacomodar(){

        }

        public bool Vacia(){
            return cantidad==0;
        }

        public Nodo? GetPrimero(){
            return this.primero;
        }

        /*public void Clock(){
            Nodo? aux = primero;

            while(aux!=null){
                aux.GetPaciente().Clock();
                aux = aux.Siguiente;
            }
        }
        */


        public int GetCantidad(){
            return cantidad;
        }


        
    }
}