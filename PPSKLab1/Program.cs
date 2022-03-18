using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PPSKLab1
{
    class EthKlient
    {
        static void Main(string[] args)
        {


            int menuNumber = 0;
            int napieciemin, napieciemax, pradmin, pradmax;

            Connection connection = new Connection();
            TcpClient client = connection.startServerConnection(10000);
            TcpClient clientt = connection.startServerConnection(10001);

            NetworkStream stream = connection.streamServer(client);
            NetworkStream streamm = connection.streamServer(clientt);

            BinaryWriter Swrite = connection.binaryWriterServer(stream);
            BinaryReader Sread = connection.binaryReaderServer(stream);

            napieciemin = 5;
            napieciemax = 7;
            pradmin = 2;
            pradmax = 4;


            Console.WriteLine("ESC - wyjście z programu, 1 - stop komunikacji, 2 - start komunikacji, 3 - wyniki");




            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                {
                    if (client.Connected)
                    {
                        Swrite.Write(Convert.ToByte(1));
                        int responsep1 = Sread.ReadInt16();
                        Console.WriteLine("\nOdb: {0}", responsep1);

                        Swrite.Write(Convert.ToByte(2));
                        int responsep2 = Sread.ReadInt16();
                        Console.WriteLine("Odb: {0}", responsep2);

                        if(responsep1 >= napieciemin && responsep1 <= napieciemax)
                        {
                            streamm.WriteByte(1);
                        }
                        else
                        {
                            streamm.WriteByte(2);
                        }
                        if (responsep2 >= pradmin && responsep2 <= pradmax)
                        {
                            streamm.WriteByte(11);
                        }
                        else
                        {
                            streamm.WriteByte(22);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n Brak połączenia");
                        Console.WriteLine("\n Błąd połączenia z symulatorem pomiarów");

                    }


                    Thread.Sleep(1000);

                    if (Console.KeyAvailable){
                        if (Console.ReadKey(true).Key == ConsoleKey.D1)
                        {
                            menuNumber = 1;
                        }
                        else if (Console.ReadKey(true).Key == ConsoleKey.D2)
                        {
                            menuNumber = 2;
                        }
                        else if (Console.ReadKey(true).Key == ConsoleKey.D3)
                        {
                            menuNumber = 3;
                        }
                        switch (menuNumber)
                        {
                            case 1:
                                Console.WriteLine("\nStop komunikacji");
                                connection.stopServerConnection(client, stream);
                                break;
                            ///
                            case 2:
                                if (!client.Connected) { 
                                Console.WriteLine("\nStart komunikacji");
                                client = connection.startServerConnection(10000);
                                stream = connection.streamServer(client);
                                Swrite = connection.binaryWriterServer(stream);
                                Sread = connection.binaryReaderServer(stream);
                                Console.WriteLine("\n Wznowiono połączenie z symulatorem pomiarów / wykonawczym");
                                }
                                break;
                            ///
                            case 3:
                                Console.WriteLine("Podaj dolną granicę napięcia: ");
                                napieciemin = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Podaj górną granicę napięcia: ");
                                napieciemax = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Podaj dolną granicę prądu: ");
                                pradmin = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Podaj górną granicę prądu: ");
                                pradmax = Convert.ToInt32(Console.ReadLine());
                                break;
                            ///
                            default:
                       
                                Console.WriteLine("default");
                                break;

                        }
                    }

                }

            }

        }
    }
}


