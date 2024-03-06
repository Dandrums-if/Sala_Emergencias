using System.Collections;
using System.Globalization;
using System.Reflection;

namespace hospital{


    public class Emergencias{
        
        //Arreglo de colas
        Cola[] Colas = new Cola[10];

        //Medicos: med1->adultos, med2->niños
        private Medico med1, med2;


//Cantidad total de pacientes. Aumenta al atender, nunca disminuye.
#region Cantidades totales

        private int total, TotalNiños, TotalAdultos;

#endregion

//Cantidad de personas en un intante de tiempo. Disminuye al atender
//Aumenta al ingresar.
#region Cantidades instantáneas
        private int cantidad, niños, adultos;
#endregion
        

 private int tiempo = 0;

        public Emergencias(){
            
            med1 = new();
            med2 = new();

            total = TotalNiños = TotalAdultos = cantidad = niños = adultos = 0;
            int op = 0;
            
            for(int i = 0; i < 10; i++){
                Cola cola = new();
                Colas[i] = cola;
            }

            while(op!=4){
                Console.WriteLine("******SALA DE EMERGENCIAS******");
                Console.WriteLine("1. Ingresar Paciente");
                Console.WriteLine("2. Atender");
                Console.WriteLine("3. Reporte");
                Console.WriteLine("4. Finalizar jornada");
                Console.Write("\nOpcion: ");
                try{
                    op = int.Parse(Console.ReadLine()!);

                    switch(op){
                        case 1:
                            this.AggPaciente();
                            break;
                        case 2:
                            this.Atender();
                            break;
                        case 3:
                            Console.WriteLine(this.GetReporte());
                            break;
                        case 4:
                            Console.WriteLine("\n\nTotal de pacientes atendidos en la jornada: "+total);
                            Console.WriteLine("Total de niños atendidos: "+TotalNiños);
                            Console.WriteLine("Total de adultos atendidos: "+TotalAdultos);

                            break;

                        default:
                            Console.WriteLine("Valor invalido");
                            break;

                    }
                }catch(Exception){
                    Console.WriteLine("ERROR");
                }


                Console.WriteLine("...");
                Console.ReadLine();
            }

            
        }


        /*Obtiene un reporte del estado de la cola:
            *Quién está siendo atendido
            *Cuántos hay en una cola específica
            *Cuántos hay en total
        */
        public string GetReporte(){
            string reporte;
            reporte = "\nPacientes en colas: " + cantidad;
            reporte = reporte + "\n\nADULTOS\n* Accidentes: " + Colas[9].GetCantidad();
            reporte = reporte + "\n* Infartos: " + Colas[8].GetCantidad();
            reporte = reporte + "\n* Respiratorios: " + Colas[7].GetCantidad();
            reporte = reporte + "\n* Partos: " + Colas[6].GetCantidad();
            reporte = reporte + "\n* Normales: " + Colas[5].GetCantidad();

            reporte = reporte + "\n\nNIÑOS\n* Accidentes: " + Colas[4].GetCantidad();
            reporte = reporte + "\n* Infartos: " + Colas[3].GetCantidad();
            reporte = reporte + "\n* Respiratorios: " + Colas[2].GetCantidad();
            reporte = reporte + "\n* Partos: " + Colas[1].GetCantidad();
            reporte = reporte + "\n* Normales: " + Colas[0].GetCantidad();
            
            if(!(med1.Ocupado())){
                reporte = reporte + "\n\nMedico de adultos esta atendiendo a: NADIE";
            }else{
                reporte = reporte + "\n\nMedico de adultos está atendiendo: " + med1.GetPaciente()!.GetRazon();
            }
            if(!(med2.Ocupado())){
                reporte = reporte + "\n\nMedico de niños esta atendiendo a: NADIE";
            }else{
                reporte = reporte + "\n\nMedico de niños está atendiendo: " + med2.GetPaciente()!.GetRazon();
            }

            return reporte;
        }

        public void AggPaciente(){

            Paciente p = Registro();
            Nodo nuevo = new(p);


            //edad es 0 ó 5
            //prioridad es 0, 1, 2, 3, 4

            //Si la edad es 0, se insertará en alguna de las primeras cuatro posiciones
            //que corresponden a los niños

            //Si la edad es 5, se insertarán en esas posiciones más 5
            //Es decir: 5, 6, 7, 8, 9
            //que corresponden a los adultos 

            int indice = p.GetEdad() + p.GetPrior();

            
            //Retorna false si la cola está llena, de lo contrario true
            bool flag = Colas[indice].Insertar(nuevo);
            if(flag){
                cantidad++;
                if(p.GetEdad()==0) niños++;
                else if(p.GetEdad()==5) adultos++;

                Console.WriteLine("Ingresa paciente "+p.GetRangoEdad()+" con "+p.GetRazon());
            }else{
                Console.WriteLine("Llega paciente "+p.GetRangoEdad()+" con "+p.GetRazon());
                Console.WriteLine("Lo sentimos, la cola está llena");
            }
            
        }

        public void Atender(){
            
            //Selecciona niños o adultos
            int indice = Random2();

            switch(indice){
                case 0:
                    if(niños==0){
                        if(adultos==0){
                            Console.WriteLine("No hay pacientes por atender");
                        }else{
                            //Si no hay niños, pero sí adultos, se atiende un adulto
                            Asignar(5);
                        }
                    }else{
                        Asignar(indice);
                    }
                    break;
                case 5:
                    if(adultos==0){
                        if(niños==0){
                            Console.WriteLine("No hay pacientes por atender");
                        }else{
                            //Si no hay adultos, pero sí niños, se atiende un niño
                            Asignar(0);
                        }
                    }else{
                        Asignar(indice);
                    }
                    break;
            }
        }

        private void Asignar(int indice){


            //Recorre una sección del arreglo de colas (niños/Adultos)
            //desde el indice mayor al menor
            //Se atiende al primero que se encuentre en este recorrido
            for( int i = indice + 4; i>=indice ; i--){

                if(!(Colas[i].Vacia())){
                    Paciente p = Colas[i].Remover()!.GetPaciente();

                        //Se asigna el paciente al médico correspondiente
                    if(indice==5){
                        med1.SetPaciente(p);
                        TotalAdultos++;
                        adultos--;

                    }else if(indice==0){
                        med2.SetPaciente(p);
                        TotalNiños++;

                        niños--;
                    }
                    cantidad--;

                    Console.WriteLine("Entra a consulta paciente "+p.GetRangoEdad()+" con "+p.GetRazon());

                    total++;

                    break;
                }
            }
        }

        /*Aumenta el tiempo transcurrido
        public void Clock(){
            tiempo++;
        }

        public int GetTiempo(){
            return tiempo;
        }
        */

        public Paciente Registro(){
            
            
            int prior = Random();
            int edad = Random2();
            Paciente p = new(prior,edad);

            return p;
        }


        //Genera un numero aleatorio que sirve de indice para la lista de prioridades
        public int Random(){
            var seed = Environment.TickCount;
            var random = new Random(seed);

            int valor = random.Next(0, 4);

            return valor;
        }
        
        //Genera un número aleatorio (0 ó 5) que sirve para dividir el arreglo de colas en 2
        //La primera mitad, 0, corresponde a los niños
        //La segunda mitad, 5, corresponde a los adultos
        public int Random2(){

            int[] ar = {0,5,0,5,0,5,0,5};
            var seed = Environment.TickCount;
            var random = new Random(seed);

            int valor = random.Next(0,7);
            
            return ar[valor];

        }
    }
}