using System;
using System.Threading;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Timers;


/*public class E1
{   
    static volatile int recurso ;
    static volatile bool producido = false;
    static Random r = new Random();


    public static void Main(string[] args)
    {   
        new Thread(Productor).Start();
        new Thread(Consumidor).Start();
    }

    public static void Productor() {

        
        recurso = r.Next(0, 100);
        Console.WriteLine("Producido: " + recurso);
        producido = true;

    
    }
    public static void Consumidor() {

        while (!producido) ;
        Console.WriteLine("Consumido: " + recurso);
                       
    }
}*/

/*public class E2
{   
    static volatile int recurso  = 0;
    static SemaphoreSlim sem1 = new SemaphoreSlim(0);
    static SemaphoreSlim sem2 = new SemaphoreSlim(1);

    public static void Main(string[] args)
    {   
        new Thread(Productor).Start();
        new Thread(Consumidor).Start();
    }

    public static void Productor() {

        while (true) { 
        
        sem2.Wait();

        recurso++;
        Console.WriteLine("Producido: " + recurso);

        sem1.Release();

        }

    }
    public static void Consumidor() {
        while (true) {
            sem1.Wait();
            Console.WriteLine("Consumido: " + recurso);
            sem2.Release();
        }
    }


}*/

/*public class E3y4
{   
    static new Random r = new Random();
    static volatile int msg;

    static SemaphoreSlim sem1 = new SemaphoreSlim(0);
    static SemaphoreSlim sem2 = new SemaphoreSlim(1);
    static SemaphoreSlim sem3 = new SemaphoreSlim(0);
    

    public static void Main(string[] args)
    {   
        new Thread(Cliente).Start();
        new Thread(Servidor).Start();
    }

    public static void Cliente() {

        while (true) { 

            sem2.Wait();            
            msg = r.Next(0, 100);
            Console.WriteLine("Cliente envia: " + msg);
            //Thread.Sleep(1000);
            sem1.Release();

            sem3.Wait();
            Console.WriteLine("Cliente recive: " + msg);
        }

    }
    public static void Servidor() {
        while (true) {
           
            sem1.Wait();
            Console.WriteLine("Servidor recive: " + msg);
            msg = msg + 1;
            Console.WriteLine("Servidor envia: " + msg);
            sem2.Release();
            sem3.Release();


        }
    }


}*/

/*public class E5 { 

    static SemaphoreSlim sem = new SemaphoreSlim(1);

    public static void Main(string[] args)
    {   
        for (int i = 0; i < 3; i++) {
            
            new Thread(Museo).Start();
        }

       
    }
    

    public static void Museo() {

        while (true) {


           sem.Wait();
           //Console.WriteLine(Thread.CurrentThread.Name);
           Console.WriteLine("Hola");
           Console.WriteLine("“qué bonito");
           Thread.Sleep(1000);
           Console.WriteLine("alucinante");
           Console.WriteLine("adios");
           
           sem.Release();

           Console.WriteLine("paseo");

        }

        
    }

}*/

/*public class E6 { 

    static Mutex mut1 = new Mutex();
    

    static int personas = 0;

    public static void Main(string[] args)
    {   
        for (int i = 0; i < 10; i++) {
            
            new Thread(Museo).Start();
        }

       
    }
    

    public static void Museo() {

        while (true) {


           mut1.WaitOne();
           personas++;
           Console.WriteLine("Hola, somos: " + personas);
           mut1.ReleaseMutex();

           //Console.WriteLine(Thread.CurrentThread.Name);
           
           Console.WriteLine("“qué bonito");
           Thread.Sleep(1000);
           Console.WriteLine("alucinante");
           
           
           mut1.WaitOne();
           personas--;
           Console.WriteLine("Adios a los: " + personas);
           mut1.ReleaseMutex();

           Console.WriteLine("paseo");

        }

        
    }

}*/

/*public class E7
{

    static Mutex mut1 = new Mutex();


    static int personas = 0;

    public static void Main(string[] args)
    {
        for (int i = 0; i < 10; i++)
        {

            new Thread(Museo).Start();
        }


    }


    public static void Museo()
    {

        bool regalo = false;

        while (true)
        {


            mut1.WaitOne();

            if (personas == 0)
            {
                regalo = true;
            }

            personas++;
            Console.WriteLine("Hola, somos: " + personas);
            mut1.ReleaseMutex();

            if (regalo)
            {
                Console.WriteLine("Tengo Regalo");
            }
            else
            {
                Console.WriteLine("No tengo regalo");
            }

            //Console.WriteLine(Thread.CurrentThread.Name);

            Console.WriteLine("“qué bonito");
            Thread.Sleep(1000);
            Console.WriteLine("alucinante");


            mut1.WaitOne();
            personas--;
            Console.WriteLine("Adios a los: " + personas);
            regalo = false;
            mut1.ReleaseMutex();

            Console.WriteLine("paseo");

        }


    }

}*/

/* Ej 8.1 si se podria dar el caso en la situacion en la que uno de los hilos ponga fin a true e imediamente despues el otro hilo ponga fin a false */

/* Ej 8.2 a) no sincornizarian correctamente ya que no se vuelven a poner a false la variable flag por lo cual el hilo dos no volveria a entrar a ala espera activa
 *  b) Se esta dando una sincronización condicional.  El proceso 1 esta siempre en ejecucion y el proceso 2 puede estas en ejecucion o en espera activa */

/*Ej 8.3 Par que no se den condiciones de carrera. */

/*Ej 8.4 El problema de la exlcusion mutua surge cuando en un programa concurrente los procesos acceden y modifican simultaneamente una variable compartida lo que provoca resultados impredecibles 
 * (condiciones de carrera).  La solucion es bloquear o limitar el acceso a la variable en las secciones criticas mediante mecanismos de sincronización*/

/*Ej 8.7 Las propiedas de seguridad consisten en grantizar que el programa se ejecute de forma correcta. Dos propiedas son la exclusion mutua y la ausencia de interbloqueopasivo*/

/*Ej 8.8  El interbloqueo de procesos se da cuando dos o mas procesos estan esperando para acceder a un recurso que esta en posesion de otro. Esta situacion impide que los procesos progresen adecuadamente*/

/*Ej. 8.9  Planificiacion:  es el proceso de seleccionar que proceso se ejecuta en cada momento
            Sincronizaciión: mecanismo utlizado para coordinar el acceso de los procesos a los recursos compartidos */

/*Ej. 8.10 Scheduling: Proceso mediante el cual el sistema operativo decide qué tarea o proceso se ejecutará a continuación en el procesador.
 *         Dispatching: Proceso mediante el cual se carga y se inicia la ejecución de un proceso en el procesador.*/

/*public class Ej9
{
    private const int N_FRAGMENTOS = 10;
    private const int N_HILOS = 3;
    private static volatile int[] fichero = new int[N_FRAGMENTOS];
    private static volatile int descargados = -1;
    static Random _random = new Random();

    static SemaphoreSlim sem1 = new SemaphoreSlim(1);
    static SemaphoreSlim sem2 = new SemaphoreSlim(1);

    private static int DescargaDatos(int numFragmento)
    {
        Thread.Sleep(_random.Next(1000));
        return numFragmento * 2;
    }
    private static void MostrarFichero()
    {
        Console.WriteLine("--------------------------------------------------");
        Console.Write("File = [");
        for (int i = 0; i < N_FRAGMENTOS; i++)
        {
            Console.Write(fichero[i] + ",");
        }
        Console.WriteLine("]");
    }
    public static void Downloader()
    {
        int fragmento = 0;
        int result = 0;
        while (descargados < N_FRAGMENTOS)
        {
            /*
             * sem1.Wait();
             if(descargados >= N_FRAGMENTOS){
                sem1.Release();
                break;        
               }
              descargados++; 
              fragmento = descargados;
             sem1.Release();
             result = DescargaDatos(fragmento);
             fichero[fragmento] = result;

             if (fragmento == N_FRAGMENTOS)
                {
                    MostrarFichero();
                }

             *

            sem1.Wait();
            descargados++;
            fragmento = descargados;
            sem1.Release();

            if (fragmento < N_FRAGMENTOS)
            {
                result = DescargaDatos(fragmento);
                
                //sem2.Wait();
                fichero[fragmento] = result;
                //sem2.Release();

                /*Ej 9.b
                 * 
                 * if (fragmento == N_FRAGMENTOS)
                {
                    MostrarFichero();
                }
                *
            }
            


        }


    }

    public static void Main(String[] args)
    {
        Thread[] threads = new Thread[N_HILOS];
        for (int i = 0; i < N_HILOS; i++)
        {
            threads[i] = new Thread(Downloader);
            threads[i].Start();
        }
        for (int i = 0; i < N_HILOS; i++)
        {
            threads[i].Join();
        }
        MostrarFichero();
    }
}*/

/*public class Ej10
{
    static SemaphoreSlim semA = new SemaphoreSlim(0);
    static SemaphoreSlim semD = new SemaphoreSlim(0);
    static Barrier barrier = new Barrier(3);
    static Barrier barrier1 = new Barrier(3);   
    static Random r = new Random();

    public static void Main(string[] args)
    {
        new Thread(AB).Start();
        new Thread(CDE).Start();
        new Thread(FG).Start();
    }


    public static void AB()
    {
        while (true)
        {
            //Thread.Sleep(r.Next(1000));

            barrier1.SignalAndWait();

            semA.Wait();
            Console.Write("A");
            //Thread.Sleep(r.Next(1000));
            Console.Write("B");
            barrier.SignalAndWait();

        }
    }
    public static void CDE()
    {
       
         while (true) {

            //Thread.Sleep(r.Next(1000));

            barrier1.SignalAndWait();

            Console.Write("C");
            semA.Release();

            //Thread.Sleep(r.Next(1000));

            semD.Wait();
            Console.Write("D");

            //Thread.Sleep(r.Next(1000));

            barrier.SignalAndWait();
            Console.Write("E");
            Console.WriteLine("");
            
         }
            
       
       
    }

    public static void FG()
    {   
        while (true)
        {
            //Thread.Sleep(r.Next(1000));

            barrier1.SignalAndWait();

            Console.Write("F");

            //Thread.Sleep(r.Next(1000));

            semD.Release();
            Console.Write("G");
            barrier.SignalAndWait();
        }
        
    }


}*/

/*public class Ej11y12
{
    static Random r = new Random();
    static int msg;
    static SemaphoreSlim sem1 = new SemaphoreSlim(0);
    static SemaphoreSlim sem2 = new SemaphoreSlim(0);
    static SemaphoreSlim sem3 = new SemaphoreSlim(0);
    static SemaphoreSlim sem4 = new SemaphoreSlim(0);
    static SemaphoreSlim sem5 = new SemaphoreSlim(1);
    static int i = 0;



    public static void Main(string[] args)
    {

        Thread T1 = new Thread(Ej11.Cliente);
        Thread T2 = new Thread(Ej11.Servidor);
        Thread T3 = new Thread(Ej11.Proxy);


        T1.Start();
        T2.Start();
        T3.Start();

        T1.Join();
        Console.WriteLine("Fin 1 ");
        if (!T1.IsAlive)
        {
            Console.WriteLine("Muere 1 ");

           
                if (T2.IsAlive)
                {

                    T2.Interrupt();
                }

                if (T3.IsAlive)
                {

                    T3.Interrupt();
                }

        }
           

      T1.Join();
      Environment.Exit(0);

    }

    public static void Cliente()
    {
        while (i < 10)
        {
            sem5.Wait();
            msg = r.Next(0, 100);
            Console.WriteLine("Cliente envia: " + msg);
            sem1.Release();
            //Thread.Sleep(r.Next(1000));

            sem3.Wait();
            Console.WriteLine("Cliente recibe: " + msg);
            i++;
            sem5.Release();

        }


    }

    public static void Servidor()
    {
        while (i < 10)
        {
            Console.WriteLine("Servidor espera");
            sem2.Wait();
            //Thread.Sleep(r.Next(1000));
            // Console.WriteLine("Servidor recibe: " + msg);
            msg += 1;
            //Console.WriteLine("Servidor envia: " + msg);
            sem4.Release();

        }
    }

    public static void Proxy()
    {
        while (i < 10)
        {
            Console.WriteLine("Proxy espera");
            sem1.Wait();
            //Thread.Sleep(r.Next(1000));
            //Console.WriteLine("Proxy recibe: " + msg);
            msg += 1;
            //Console.WriteLine("Proxy envia: " + msg);
            sem2.Release();
            sem4.Wait();
            sem3.Release();
        }
    }

}*/

/*public class Ej13
{
    private const int N_TRENES = 2;
    private const int N_TRAMOS = 100;
    private static volatile Random random = new Random();
    static  SemaphoreSlim[] semphores = new SemaphoreSlim[N_TRAMOS];


    private static void Tren(object numTren)
    {   
        ##
        sem1.Wait();
        Thread.Sleep(random.Next(500));
        RecorrerTramoA((int)numTren);
        sem1.Release();

        sem2.Wait();
        Thread.Sleep(random.Next(500));
        RecorrerTramoB((int)numTren);
        sem2.Release();

        sem3.Wait();
        Thread.Sleep(random.Next(500));
        RecorrerTramoC((int)numTren);
        sem3.Release();
        ##

        for (int i = 0; i < N_TRAMOS; i++)
        {
            semphores[i].Wait();
            Thread.Sleep(random.Next(500));
            
            RecorerTramoN((int)numTren, i);
            semphores[i].Release();
        }

    }

    private static void  RecorerTramoN(int numTren, int tramo)
    {
        WriteLineI("Entra T" + numTren + " tramo " + tramo);
        Thread.Sleep(random.Next(500));
        WriteLineI("Sale T" + numTren + " tramo " + tramo);
    }


    
    private static void RecorrerTramoA(int numTren)
    {
        WriteLineI("Entra TA T" + numTren);
        Thread.Sleep(random.Next(500));
        WriteLineI("Sale TA T" + numTren);
    }

    private static void RecorrerTramoB(int numTren)
    {
        WriteLineI("Entra TB T" + numTren);
        Thread.Sleep(random.Next(500));
        WriteLineI("Sale TB T" + numTren);
    }

    private static void RecorrerTramoC(int numTren)
    {
        WriteLineI("Entra TC T" + numTren);
        Thread.Sleep(random.Next(500));
        WriteLineI("Sale TC T" + numTren);
    }

    private static void Main()
    {   
        for(int i = 0; i < N_TRAMOS; i++)
        {
            semphores[i] = new SemaphoreSlim(1);
        }


        for (int i = 0; i < N_TRENES; i++)
        {
            Thread t = new Thread(Tren);
            t.Name = new string('\t', i*2);
            t.Start(i);
        }
    }

    private static void WriteLineI(string s)
    {
        Thread.Sleep(random.Next(10));
        Console.WriteLine(Thread.CurrentThread.Name + s);
        Thread.Sleep(random.Next(10));
    }
}*/

/*public class Ej14
{

    private const int n_procesos = 4;
     
    private static int contador = 0;
    static SemaphoreSlim sem = new SemaphoreSlim(0);
    static SemaphoreSlim sem2 = new SemaphoreSlim(1);    
    static SemaphoreSlim sem3 = new SemaphoreSlim(0);
    

    //static Barrier barrier = new Barrier(n_procesos);
   

    public static void Main(string[] args)
    {
        for (int i = 0; i < n_procesos; i++)
        {
            new Thread(Proceso).Start();
        }   
    }

    public static void Proceso()
    {

        while (true)
        {
            Console.Write("A");
            //barrier.SignalAndWait();

            
            sem2.Wait();
            contador++;
            sem2.Release();

            if (contador == n_procesos)
            {
                contador = 0;
                sem.Release(n_procesos - 1);

            }
            else
            {
                sem.Wait();
            }   
            

            Console.Write("B");
            //barrier.SignalAndWait();

            
            sem2.Wait();
            contador++;
            sem2.Release();

            if (contador == n_procesos)
            {
                contador = 0;
                sem3.Release(n_procesos - 1);

            }
            else
            {
                sem3.Wait();
            }
            


            //Thread.Sleep(2000);
        }

    }


}*/


/*public class Ej15
{   
    //static Barrier barrier = new Barrier(4);
    static SemaphoreSlim sem = new SemaphoreSlim(4,4);

    static SemaphoreSlim sem2 = new SemaphoreSlim(1);
    static SemaphoreSlim sem3 = new SemaphoreSlim(0);

    private static int contador = 0;    

    public static void Main(string[] args)
    {

        new Thread(ProcesoA).Start();
        new Thread(ProcesoB).Start();
        new Thread(ProcesoC).Start();
        new Thread(ProcesoD).Start();

    }

    public static void ProcesoA()
    {
        while (true)
        {   
            sem.Wait();
            Console.Write("A");
            //barrier.SignalAndWait();
            sem2.Wait();
            contador++;
            sem2.Release(); 

            if (contador == 4)
            {
                contador = 0;
                sem3.Release(3);
            }
            else
            {
                sem3.Wait();
            }


            Console.WriteLine("-");
            sem.Release(4);
           
            
        }
    }

    public static void ProcesoB()
    {
        while (true)
        {   
            sem.Wait();
            Console.Write("B");
            //barrier.SignalAndWait();

            sem2.Wait();
            contador++;
            sem2.Release();

            if (contador == 4)
            {
                contador = 0;
                sem3.Release(3);
            }
            else
            {
                sem3.Wait();
            }

        }
    }

    public static void ProcesoC()
    {
        while (true)
        {   
            sem.Wait();
            Console.Write("C");
            //barrier.SignalAndWait();

            sem2.Wait();
            contador++;
            sem2.Release();

            if (contador == 4)
            {
                contador = 0;
                sem3.Release(3);
            }
            else
            {
                sem3.Wait();
            }
        }
    }

    public static void ProcesoD()
    {
        while (true)
        {   
            sem.Wait();
            Console.Write("D");
            //barrier.SignalAndWait();

            sem2.Wait();
            contador++;
            sem2.Release();

            if (contador == 4)
            {
                contador = 0;
                sem3.Release(3);
            }
            else
            {
                sem3.Wait();
            }


        }
    }


}*/

/*public class Ej16(YO)
{
    private const int N_FRAGMENTOS = 10;
    private const int N_HILOS = 3;
    private static volatile int[] fichero = new int[N_FRAGMENTOS];
    private static volatile int descargados = 0;
    static Random _random = new Random();

    static SemaphoreSlim sem1 = new SemaphoreSlim(1);
    static SemaphoreSlim sem2 = new SemaphoreSlim(1);



    private const int N_FICHEROS = 20;
    static SemaphoreSlim sem3 = new SemaphoreSlim(N_HILOS);
    private static int contador = 0;
    static Barrier barrier = new Barrier(N_HILOS);

    private static int DescargaDatos(int numFragmento)
    {
        Thread.Sleep(_random.Next(500));
        return numFragmento * 2;
    }

    private static void MostrarFichero()
    {
        Console.WriteLine("--------------------------------------------------");
        Console.Write("File = [");
        for (int i = 0; i < N_FRAGMENTOS; i++)
        {
            Console.Write(fichero[i] + ",");
            fichero[i] = 0;
        }
        Console.WriteLine("]");
        descargados = 0;
        contador++;
        sem3.Release(N_HILOS);
    }


    public static void Downloader()
    {
        int fragmento = 0;
        int result = 0;

        while (contador < N_FICHEROS )
        {
            sem3.Wait();

            if (contador >= N_FICHEROS)
            {

                break;
            }
                        
            
            while (descargados <= N_FRAGMENTOS )
            {

                sem1.Wait();

                if (descargados == N_FRAGMENTOS)
                {
                    sem1.Release();
                    break;
                }

                
                fragmento = descargados;
                descargados++;

                sem1.Release();
                  
                result = DescargaDatos(fragmento);
                fichero[fragmento] = result;

                   // Console.WriteLine("Fragmento: " + fragmento + " Resultado: " + result);
              
            }

            barrier.SignalAndWait();
            if (fragmento == (N_FRAGMENTOS - 1))
            {
                MostrarFichero();
            }

        }

    }

    public static void Main(String[] args)
    {



        Thread[] threads = new Thread[N_HILOS];
        for (int i = 0; i < N_HILOS; i++)
        {
            threads[i] = new Thread(Downloader);
            threads[i].Start();
        }
        for (int i = 0; i < N_HILOS; i++)
        {
            threads[i].Join();
        }

        //MostrarFichero();
        Environment.Exit(0);

    }
}*/


/*public class Ej16 (Profe)
{

    private const int N_FRAGMENTOS = 10;
    private const int N_HILOS = 3;
    private static volatile int[] fichero = new int[N_FRAGMENTOS];
    private static readonly Random random = new Random();

    private static volatile int fragmentoPendienteDescargar = 0;
    private static volatile int hilosTerminados = 0;
    private static Mutex mutex = new Mutex();
    private static Mutex hilos = new Mutex();



    private const int N_FICHEROS = 10;
    static SemaphoreSlim sem3 = new SemaphoreSlim(N_HILOS);
    private static int contador = 0;
    //static Barrier barrier = new Barrier(N_HILOS);


    private static int DescargaDatos(int numFragmento)
    {
        random.Next(1000);
        return numFragmento * 2;
    }

    private static void MostrarFichero()
    {
        WriteLine("--------------------------------------------------");
        Console.Write("File = [");
        for (int i = 0; i < N_FRAGMENTOS; i++)
        {
            Console.Write(fichero[i] + ",");
            fichero[i] = 0; 
        }

        WriteLine("]");

        contador++;
        fragmentoPendienteDescargar = 0;
        hilosTerminados = 0;
        sem3.Release(N_HILOS);
    }

    private static void DescargaFragmentos()
    {


        while (true)
        {
            mutex.WaitOne();
            if (fragmentoPendienteDescargar == N_FRAGMENTOS)
            {
                mutex.ReleaseMutex();
                break;
            }

            int fragmentoParaDescargar = fragmentoPendienteDescargar;
            fragmentoPendienteDescargar++;
            mutex.ReleaseMutex();

            //WriteLine("Descargando el fragmento " + fragmentoParaDescargar);
            int datosDescargados = DescargaDatos(fragmentoParaDescargar);
            //WriteLine("Guardando el fragmento " + fragmentoParaDescargar);
            fichero[fragmentoParaDescargar] = datosDescargados;
        }

    }

    private static void Downloader()
    {

        while (contador < N_FICHEROS)
        {
            sem3.Wait();

            DescargaFragmentos();

            bool mostrarFichero = false;

            hilos.WaitOne();
            hilosTerminados++;
            mostrarFichero = hilosTerminados == N_HILOS;
            hilos.ReleaseMutex();

            if (mostrarFichero)
            {
                MostrarFichero();
            }

        }
    }

    public static void Main()
    {
        Thread[] threads = new Thread[N_HILOS];
        for (int i = 0; i < N_HILOS; i++)
        {
            threads[i] = new Thread(Downloader);
            threads[i].Start();
        }

        for (int i = 0; i < N_HILOS; i++)
        {
            threads[i].Join();
        }


    }

    private static void WriteLine(string s)
    {
        random.Next(10);
        Console.WriteLine(s);
        random.Next(10);
    }
}*/

/*public class Ej17
{

    const int N_FILOSOFOS = 5;
    static volatile Random random = new Random();
    static SemaphoreSlim[] tenedores = new SemaphoreSlim[N_FILOSOFOS];

    private static void Filosofo(object numFilosofo)
    {
        int i = (int)numFilosofo;
        int izq = i;
        int der = (i + 1) % N_FILOSOFOS;



        //while (true)
        for (int j = 0; j < 10; j++)
        {
            WriteLineI("Pensar");
            // Obtener tenedores

            // Obtener tenedores
            if (i % 2 == 0)
            {
                tenedores[izq].Wait();
                tenedores[der].Wait();
            }
            else
            {
                tenedores[der].Wait();
                tenedores[izq].Wait();
            }

            //Thread.Sleep(random.Next(1000));
            WriteLineI("Comer");

            Thread.Sleep(random.Next(1000));
            // Liberar tenedores
            WriteLineI("SoltarI");
            tenedores[izq].Release();

            WriteLineI("SoltarD");
            tenedores[der].Release();
            


        }
    }
    private static void Main()
    {

        for (int i = 0; i < N_FILOSOFOS; i++)
        {
            tenedores[i] = new SemaphoreSlim(1,1);
        }

        for (int i = 0; i < N_FILOSOFOS; i++)
        {
            Thread t = new Thread(Filosofo);
            t.Name = new string('\t', i);
            t.Start(i);
        }
    }
    private static void WriteLineI(string s)
    {
        Thread.Sleep(random.Next(10));
        Console.WriteLine(Thread.CurrentThread.Name + s);
        Thread.Sleep(random.Next(10));
    }



}*/

/* 18.1 ) El metodo wait() mira el numero de permisos disponibles, si este es 0 bloquea el hilo a la espera de que se libere un permiso, 
  en el caso de que sea superior a 0 decrementa el numero de permisos en uno y continua la ejecucion del hilo.
   
  El metodo release() incrementa el numero de permisos en uno , mientras no sea superior al numero maximo de permisos,
  y libera un hilo bloqueado a la espera de un permiso el cual consume dicho perimiso inmediatamente.

    18.2) Usando un semaforo con un numero de permisos igual a 1, haciendo un wait antes de entrar en la seccion critica(uso de la variable) y un release al salir de la misma.

    18.4) PREGUNTAR!!!!
    
    18.5) a) 1
          b) 1
          c) K

    18.6) Porque son instrucciones atomicas de grano grueso.
    
    
 
 */

/*class Ejercicio18_7
{
    const int N_PROCESOS = 4;
    static SemaphoreSlim sem = new SemaphoreSlim(1);
    static SemaphoreSlim sem1 = new SemaphoreSlim(0);  
    static int contador = 0;
    static Random r = new Random();

    private static void Proceso()
    {
        while (true)
        {
            Console.Write("A");
            Thread.Sleep(r.Next(1000));
            PuntoDeSincronizacion();
            Console.Write("B");
            Thread.Sleep(r.Next(1000));
            PuntoDeSincronizacion();
        }
        
    }
    private static void PuntoDeSincronizacion()
    {
        sem.Wait();    
        contador++;
       

        if (contador == N_PROCESOS)
        {   

            contador = 0;
            sem.Release();            
            sem1.Release(N_PROCESOS - 1);
            
        }
        else
        {
            sem.Release();
            sem1.Wait();
        }

        


    }
    private static void Main()
    {
        for (int i = 0; i < N_PROCESOS; i++)
        {
            Thread t = new Thread(Proceso);
            t.Name = new string('\t', i);
            t.Start();
        }
    }
}
*/
/* 18.8) No ya que la instruccion wait es atomica y es necesario que el contador de permisos este a 0 para poder bloquear un hilo*/

/*class Ejercicio18_9
{   

    static SemaphoreSlim sem = new SemaphoreSlim(1);
    static SemaphoreSlim sem1 = new SemaphoreSlim(0);
    static SemaphoreSlim sem2 = new SemaphoreSlim(0);
    static SemaphoreSlim sem3 = new SemaphoreSlim(1);

    static int contador = 0;

    public static void Main(string []args)
    {
        new Thread(PA).Start();
        new Thread(PBC).Start();
        new Thread(PD).Start();
    }

    public static void PA()
    {   
        sem1.Wait();
        Console.WriteLine("A");
        
        barrera();


    }

    public static void PBC()
    {
        Console.WriteLine("B");
        sem1.Release(2);


        sem2.Wait();          


        Console.WriteLine("C");
    }

    public static void PD()
    {   
        sem1.Wait();
        Console.WriteLine("D");
        barrera();
        
    }


    public static void barrera() {

        sem3.Wait();
        contador++;

        if (contador == 2)
        {
            sem2.Release();
        }

        sem3.Release();

    }
}
*/

/*class Ejercicio18_10
{
    private static int x;
    private static SemaphoreSlim sincronizacion;
    private static SemaphoreSlim exclusion;


    public static void P1()
    {
        exclusion.Wait();
        x++;
        if (x == 2)
        {
            sincronizacion.Release();
        }
        exclusion.Release();
    }
    public static void P2()
    {
        exclusion.Wait();
        x++;
        if (x == 1)
        {
            exclusion.Release();

            sincronizacion.Wait();
        }
        else
        {
            exclusion.Release();
        }
        
    }
    private static void Main()
    {
        x = 0;
        sincronizacion = new SemaphoreSlim(0);
        exclusion = new SemaphoreSlim(1);
        new Thread(P1).Start();
        new Thread(P2).Start();
    }
}

Si el hilo P2 entra primero se queda bloqueado no liberando la exclusion mutua y no permitiendo que el hilo P1 entre por lo que se produce un interbloqueo pasivo*/

/*class Ejercicio18_11
{
    private static int x;
    private static int y;
    private static SemaphoreSlim semY;
    private static SemaphoreSlim semX;
    private static void PA()
    {
        semX.Wait();
        x++;
        semY.Wait();
        y *= x;
        semY.Release();
        semX.Release();
    }
private static void PB()
    {
        semY.Wait();
        y++;
        semX.Wait();
        x *= y;
        semX.Release();
        semY.Release();
    }
    private static void Main()
    {
        x = 2;
        y = 3;

        semX = new SemaphoreSlim(1);
        semY = new SemaphoreSlim(1);

        new Thread(PA).Start();
        new Thread(PB).Start();
    }
}*/

// Cuando ambos procesos entran a la vez se produce un interbloqueo pasivo ya que ambos procesos se quedan bloqueados esperando
// a que el otro libere el semaforo que necesita para continuar(lineas 1381 y 1390).

// La multiplicacion y*=x y x*=y no se encuentra bajo exclusion mutua por lo que se puede dar una condicion de carrera.

/*public class EJ18_12 { 
        
    static SemaphoreSlim sem = new SemaphoreSlim(0);
    static SemaphoreSlim sem2 = new SemaphoreSlim(0);
    static SemaphoreSlim sem3 = new SemaphoreSlim(0);

    static Barrier barrier = new Barrier(3);
    static Random r = new Random();
  

    public static void Main(string[] args)
    {
        new Thread(Proceso1).Start();
        new Thread(Proceso2).Start();
        new Thread(Proceso3).Start();

    }

    public static void Proceso1()
    {
        while (true)
        {   
            Thread.Sleep(r.Next(1000));

            Console.Write("A");
            sem.Release();
            sem3.Release();
            Console.Write("B");
           
            barrier.SignalAndWait();

            Console.WriteLine("");

            barrier.SignalAndWait();
        }
    }

    public static void Proceso2()
    {
        while (true)
        {

            Thread.Sleep(r.Next(1000));


            Console.Write("C");
            sem2.Release();
            sem.Wait();
            Console.Write("D");
         
            barrier.SignalAndWait();

            barrier.SignalAndWait();
        }
    }

    public static void Proceso3()
    {
        while (true)
        {

            Thread.Sleep(r.Next(1000));


            Console.Write("E");
            
            sem2.Wait();
            sem3.Wait();


            Console.Write("F");
            
            barrier.SignalAndWait();

            barrier.SignalAndWait();
        }
    }




}*/

/*class Ejercicio18_13
{
    const int N_PROCESOS = 3;
    private static volatile int nProcesos;
    private static SemaphoreSlim semB;
    private static SemaphoreSlim emNProcesos;
    public static void Proceso()
    {
        while (true)
        {
            Console.Write("A");
            PuntoDeSincronizacion();
            Console.Write("B");

        }
    }
    private static void PuntoDeSincronizacion()
    {
        emNProcesos.Wait();
        nProcesos++;
        if (nProcesos == N_PROCESOS)
        {
            nProcesos = 0;
            emNProcesos.Release(); //I0
            for (int i = 0; i < N_PROCESOS; i++)
            {
                semB.Release(); //I1
                
            }
        }
        else
        {
            emNProcesos.Release();
            semB.Wait();
        }
    }
    private static void Main()
    {
        nProcesos = 0;

        semB = new SemaphoreSlim(0);
        emNProcesos = new SemaphoreSlim(1);

        for (int i = 0; i < N_PROCESOS; i++)
        {
            new Thread(Proceso).Start();
        }
    }
}*/

//El problema principal es que la liberación de los semáforos semB no es atómica. Esto significa que entre el momento en que nProcesos se reinicia a 0 y el momento en que todos los semáforos
//semB son liberados, otros procesos pueden entrar en la función PuntoDeSincronizacion y modificar nProcesos de manera incorrecta. Tambien se esta liberando un permiso del semaforo mas de lo necesario.

/*18.14) Se podria dar un interbloqueo en el caso en el que ambos procesos entran en el primer semaforo a la vez y se bloqueen esperando a que el otro libere el semaforo X e Y que necesita cada uno para continuar , 
 * se podria solucionar eliminando uno de los semaforos o colocando el orden de los semaforos igual para ambos procesos.
 * Una salida seria x = 2, y = 3 y la otra x = 3, y = 2*/

/*18.15) Se trata de un problema de exclusion mutua generalizada*/

/*public class Ej18_15
{
    const int N_PROCESOS = 4;
    const int K = 2;
    static SemaphoreSlim semaphore = new SemaphoreSlim(K);
    
    public static void P()
    {
        while (true)
        {
            // Obtener recurso
            semaphore.Wait();
            
            // Usar recurso
            Console.WriteLine("Proceso " + Thread.CurrentThread.Name + " obtiene el recurso");
            
            // Liberar recurso
            semaphore.Release();

            // Hacer otras cosas
            
            Console.WriteLine("---------------");
        }
    }
    private static void Main()
    {
        for (int i = 0; i < N_PROCESOS; i++)
        {
            Thread t = new Thread(P);
                t.Name = i.ToString();
                t.Start();
        }
    }
}*/

/*class Ejercicio18_16
{
    private static SemaphoreSlim[] sinc = new SemaphoreSlim[2];
    private static Barrier barr = new Barrier(3);
    private static Random r = new Random();
    public static void PuntoDeSincronizacion(int numProc)
    {
        if (numProc == 0)
        {
            sinc[0].Release();
            sinc[1].Release();
        }
        else
        {
            if (numProc == 1)
            {
                sinc[1].Release();
                sinc[0].Wait();
            }
            else
            {
                sinc[1].Wait();
                sinc[1].Wait();
            }
        }
    }
    public static void Proceso(object numProc)
    { while (true)
        {
            Thread.Sleep(r.Next(1000));

            Console.Write("A" + numProc + " ");
            PuntoDeSincronizacion((int)numProc);
            Thread.Sleep(r.Next(1000));
            Console.Write("B" + numProc + " ");

            barr.SignalAndWait();
            Console.WriteLine(" ");

            barr.SignalAndWait();   
        }
        
    }
    public static void Main()
    {
        sinc[0] = new SemaphoreSlim(0);
        sinc[1] = new SemaphoreSlim(0);

        for (int i = 0; i < 3; i++)
        {
            var t = new Thread(Proceso);
            t.Start(i);
        }
    }
}*/

/*class Ejercicio18_17
{
    private static SemaphoreSlim sem;
    public static void P1()
    {
        Console.Write("A");
        sem.Release();
    }
    public static void P2()
    {
        sem.Wait();
        Console.Write("B");
        sem.Release();
        sem.Release();
    }
    public static void P3()
    {
        sem.Wait();
        sem.Wait();
        Console.Write("C");
    }
    public static void Main(String[] args)
    {
        sem = new SemaphoreSlim(0);
        new Thread(P1).Start();
        new Thread(P2).Start();
        new Thread(P3).Start();
    }
}*/

// No ya que se puede producir un bloqueo en el caso de que el hilo P3 sea liberado por P1 en vez de P2 primero y se bloquee esperando a que se liberen los dos semaforos de P2 y P3

/*class Ejercicio19_plantilla
{
    //   • Cualquier número de lectores puede acceder a la vez a la BD, siempre que no haya escritores accediendo.
    //   • El acceso a la BD de los escritores es exclusivo. Mientras haya algún lector leyendo, ningún escritor puede acceder a la BD, pero otros lectores sí.
    //   • Se puede tener varios escritores trabajando, aunque estos se deberán sincronizar para que la escritura se lleve a cabo de uno en uno.
    //   • Se da prioridad a los escritores. Ningún lector puede acceder a la BD cuando haya escritores que desean hacerlo (aunque esto pueda causar inanición deLectores).
    

    const int N_LECTORES = 5;
    const int N_ESCRITORES = 3;

    static SemaphoreSlim mutexEscritores = new SemaphoreSlim(1,1);
    static SemaphoreSlim mutexLectores = new SemaphoreSlim(1,1);

    static SemaphoreSlim escritorEspera = new SemaphoreSlim(0);
    static SemaphoreSlim lectorEspera = new SemaphoreSlim(0);

    static bool escrtitoresBd, escritorPreparado;

    static int nLectores = 0;

    public static void InicioLectura()
    {
       mutexLectores.Wait();  
       if (escrtitoresBd || escritorPreparado|| lectorEspera.CurrentCount > 0)
         {
            lectorEspera.Wait();
            
         }
        nLectores++;
        mutexLectores.Release();

    }
    public static void FinLectura()
    {

        mutexLectores.Wait();
        if(escritorPreparado || escritorPreparado)
        {
            escritorEspera.Release();
        }

        nLectores--;
       
        mutexLectores.Release();
        
        
    }
    public static void InicioEscritura()
    {   
       mutexEscritores.Wait();
        if (escrtitoresBd || nLectores < 0)
        {
            escritorEspera.Wait();
            escritorPreparado = true;
        }
        
        escrtitoresBd = true;
        mutexEscritores.Release();
    }
    public static void FinEscritura()
    {
        mutexEscritores.Wait();
        escrtitoresBd = false;

        if (escritorEspera.CurrentCount > 0)
        {
            escritorPreparado = true;
            escritorEspera.Release();
        }
        else
        {
            lectorEspera.Release(N_LECTORES);
        }
        mutexEscritores.Release();


    }
    private static void Lector()
    {
        while (true)
        {
            InicioLectura();
            Console.WriteLine("Leer datos");
            FinLectura();
            Console.WriteLine("Procesar datos");
        }
    }
    private static void Escritor()
    {

        while (true)
        {
            Console.WriteLine("Generar datos");
            InicioEscritura();
            Console.WriteLine("Escribir datos");
            FinEscritura();
            Thread.Sleep(1000);
        }
    }
    private static void Main()
    {
        for (int i = 0; i < N_LECTORES; i++)
        {
            new Thread(Lector).Start();
        }
        for (int i = 0; i < N_ESCRITORES; i++)
        {
            var t = new Thread(Escritor);
            t.Name = new string('\t', i);
            t.Start();
        }
    }
}*/


// volatile es


/*class Ejercicio07
{
    const int N_Escritores = 3;
    const int N_Lectores = 5;
    static ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();
    static Random random = new Random();

  

    void Lector()
    {
        while (true)
        {
            rwl.EnterReadLock();

            try
            {
                WriteLine("Leer datos");
            }
            catch (ThreadInterruptedException ex)
            {
                WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                WriteLine("Procesar datos");
                rwl.ExitReadLock();
            }
        }
    }

    void Escritor()
    {
        while (true)
        {
            Thread.Sleep(1000);
            rwl.EnterWriteLock();

            try
            {
                WriteLine("Escribir datos");
            }
            catch (ThreadInterruptedException ex)
            {
                WriteLine("Exception: " + ex.Message);
            }
            finally
            {

                rwl.ExitWriteLock();
            }
        }

    }

    void WriteLine(string s)
    {
        Thread.Sleep(random.Next(10));
        Console.WriteLine("[{0}] {1}", Thread.CurrentThread.Name, s);
        Thread.Sleep(random.Next(10));
    }

    void Exec()
    {
        for (int i = 0; i < N_Lectores; i++)
        {
            Thread t = new Thread(() => Lector());
            t.Name = "Lector_" + i;
            t.Start();
        }

        for (int i = 0; i < N_Escritores; i++)
        {
            Thread t = new Thread(() => Escritor());
            t.Name = "Escritor_" + i;
            t.Start();
        }
    }

    static void Main()
    {
        new Ejercicio07().Exec();
    }
}*/

/*class Pila<T>
{
    private T[] elementos;
    private int numElementos;
    public Pila(int tope)
    {
        elementos = new T[tope];
        numElementos = 0;
    }
    public T GetElementos(int indice)
    {
        return elementos[indice];
    }
    public void AddElementos(T elem)
    {
        if (numElementos < elementos.Length)
        {
            elementos[numElementos] = elem;
            numElementos++;
        }
    }
}

class A
{
    public static void Main(String[] args) { 
    
        var pila = new Pila<int>(10);

        pila.AddElementos(1);
        pila.AddElementos(2);
        pila.AddElementos(3);

        Method<int>(pila);
    
    }

    public static void Method<T>(Pila<T> pila)
    {
        for (int i = 0; i < 10; i++)
        {
            
            Console.WriteLine(pila.GetElementos(i));
        }
    }
}*/

/*public class PdM
{
    static IPEndPoint serverEp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);


    public static void Main(string[] args)
    {   
        new Thread(Servidor).Start();
        new Thread(Cliente).Start();

    }

    public static void Cliente()
    {   
        string mensaje = "Cliente Hola";
        byte[] buffer = Encoding.UTF8.GetBytes(mensaje);

        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(serverEp);

        socket.Send(buffer);

        try
        {
            socket.Shutdown(SocketShutdown.Both);
        }
        finally
        {
            socket.Close();
        }
    }

    public static void Servidor()
    {
        byte[] buffer = new byte[1024];

        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind(serverEp);
        socket.Listen(1);
        Socket cliente = socket.Accept();
        
        int bytesRecibidos = cliente.Receive(buffer);
        string mensajeRecibido = Encoding.UTF8.GetString(buffer, 0, bytesRecibidos);
        IPEndPoint ip = (IPEndPoint)cliente.RemoteEndPoint;

        Console.WriteLine("Server recibido: " + mensajeRecibido + " from: " + ip.Address +" "+  ip.Port);

        try
        {
            socket.Shutdown(SocketShutdown.Both);
        }
        finally
        {
            cliente.Close();
            socket.Close();

        }
    }
}*/

/*public class MyClass
{
    static int progress = 0;
    static SemaphoreSlim semaphore = new SemaphoreSlim(0);
    static Random r = new Random();
    public static void Main(string[] args)
    {
        new Thread(proceso1).Start();
        new Thread(proceso2).Start();
    }


    public static void proceso1()
    {
        Thread.Sleep(r.Next(500));
        progress = progress + 20;
        Console.WriteLine("Proceso 1: " + progress);
        if (progress >= 20)
        {
            semaphore.Release();
        }        
        semaphore.Wait();
    }
    public static void proceso2()
    {   
        Thread.Sleep(r.Next(500));
        progress = progress + 30;
        Console.WriteLine("Proceso 2: " + progress);
        if (progress >= 30)
        {
            semaphore.Release();
        }

        Thread.Sleep(r.Next(500));
        progress = progress + 50;
        Console.WriteLine("Proceso 2: " + progress);

        if (progress >= 80)
        {
            semaphore.Release();
        }

        
        semaphore.Wait();
        
    }
}*/



namespace Populus
{




    public class Soldado { 

        public int vida;
        public int ataque;
        private Random r = new Random();
        public bool vivo = true;

        public Soldado()
        {
            vida = r.Next(100);
            ataque = r.Next(10);
        }

        public void Atacar(Soldado enemigo)
        {
            enemigo.vida -= ataque;
            if (enemigo.vida <= 0)
            {
                enemigo.Die();
            }

            Thread.Sleep(r.Next(1000));
        }

        public void Fusionar(Soldado s)
        {   
            vida += s.vida;
            ataque += s.ataque;
            s.Die();

            Thread.Sleep(r.Next(1000));
        }

        public void Die()
        {
            //destroy
            vivo = false;
        }

        public void Explorar()
        {
            Console.WriteLine("Explorando soy irrelevante");
            Thread.Sleep(r.Next(1000));
        }
    }

    public class Bando
    {
        public List<Soldado> soldados = new List<Soldado>();
        private Random r = new Random();
        public int nSoldados = 0;

        public Bando(int n)
        {
            for (int i = 0; i < n; i++)
            {
                soldados.Add(new Soldado());
                nSoldados++;
            }
        }

        public void GenerarSoldado()
        {
            while (!Game.guerra) {

                soldados.Add(new Soldado());
                nSoldados++;

                Thread.Sleep(5000/nSoldados);
            }
            
            
        }


        public void QuitarSoldado(Soldado s)
        {
            soldados.Remove(s);
            nSoldados--;
        }

        public void RealizarAccion(Bando b, Soldado s)
        {

        }
    }


    public class Game
    {   
        private Bando bando1;
        private Bando bando2;

        public static bool guerra = false;
        private static int dur = 20;
        

        public static void Main(string[] args)
        {
            Bando bando1 = new Bando(5);
            Bando bando2 = new Bando(5);

            Thread G1 = new Thread(bando1.GenerarSoldado);
            Thread G2 = new Thread(bando2.GenerarSoldado);

            G1.Start();
            G2.Start();

            
            Thread RA1 = new Thread(() => RealizarAccionesBando(bando1, bando2));
            Thread RA2 = new Thread(() => RealizarAccionesBando(bando2, bando1));

            RA1.Start();
            RA2.Start();

            DateTime dS = DateTime.Now;
            DateTime delta;

            while (true)
            {   
                delta = DateTime.Now;
                TimeSpan ts = delta - dS;

                if ((int)ts.TotalSeconds >= dur)
                {
                    guerra = true;
                }

                if (ComprobarGanador(bando1,bando2) == true)
                {   
                    try { 
                        G1.Abort();
                        G2.Abort();
                        RA1.Abort();
                        RA2.Abort();
                    }
                    catch (ThreadAbortException e)
                    {
                        Console.WriteLine("Hilo abortado");
                    }
                   
                    break;
                }
            }


        }


        public static void RealizarAccionesBando(Bando b, Bando e)
        {
            Random r = new Random();
            while(b.nSoldados > 0 )
            {   
                

                foreach (Soldado s in b.soldados)
                {
                    if (s.vivo)
                    {
                        int x;
                            if (!guerra) { 

                                x = r.Next(3);
                            }
                            else
                            {
                                x = r.Next(2);
                            }
                        

                        switch (x)
                        {
                            case 0:
                                s.Explorar();
                                break;
                            case 1:
                                s.Atacar(e.soldados[r.Next(e.nSoldados)]);
                                break;
                            case 2:
                                s.Fusionar(b.soldados[r.Next(b.nSoldados)]);
                                break;
                        }
                        
                    }
                }
            }
        }

        public static bool ComprobarGanador(Bando b1, Bando b2)
        {
            if (b1.nSoldados == 0)
            {
                Console.WriteLine("Bando 2 ha ganado");
                return true;
            }
            else if (b2.nSoldados == 0)
            {
                Console.WriteLine("Bando 1 ha ganado");
                return true;
            }
            else
            {
                return false;
            }
        }


    }

    

}

 