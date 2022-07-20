using System;


namespace EjericioFinal_AgendaConArchivos
{
    class Program
    {
        static void Main(string[] args)
        {


            string option = "-1";
  
            do
            {
                //Console.Clear();
                Menu.PintaMenu();
                Console.WriteLine("\nPor favor ingresar una opción!");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.Clear();
                        Cita.AgregarCita();
                        break;
                    case "2":
                        Console.Clear();
                        Contacto.AgregarContacto();
                        break;
                    case "3":
                        Console.Clear();
                        Cita.ConsultarCita();
                        break;
                    case "4":
                        Console.Clear();
                        Contacto.ContactoPorNombre();
                        break;
                    case "0":
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Por favor ingresar una opción válida!");
                        //Menu.PintaMenu();
                        //Console.WriteLine("\nPor favor ingresar una opción!");
                        //option = int.Parse(Console.ReadLine());
                        break;
                }
                if (option =="0") 
                {
                    Console.WriteLine("Cerrar la aplicacion?");
                    Console.WriteLine("Presionar S para salir");
                    Console.WriteLine("Presionar N para permanecer");
                    string salir = Console.ReadLine();
                    salir = salir.ToUpper();
                    if (salir != "S")
                    {
                        option = "-1";
                        if (salir != "N")
                        {
                            Console.Clear();
                            Console.WriteLine("Opcion no valida\n");
                        }
                      
                    }
                    Console.Clear();

                }

            } while (option != "0");
            
            
        }
    }
}
