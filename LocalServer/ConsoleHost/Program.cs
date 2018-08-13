﻿namespace Microsoft.LocalForwarder.ConsoleHost
{
    using System;
    using System.IO;
    using Library;

    class Program
    {
        static void Main(string[] args)
        {
            Common.Diagnostics.Log("Starting the console host...");

            Host host = new Host();

            try
            {
                Common.Diagnostics.Log("Starting the host...");

                string config = ReadConfiguratiion();

                host.Run(config, TimeSpan.FromSeconds(5));

                Common.Diagnostics.Log("The host is running");
            }
            catch (Exception e)
            {
                Common.Diagnostics.Log(FormattableString.Invariant($"Unexpected error while starting the host. {e.ToString()}"));
                throw;
            }
            finally
            {
                Console.ReadKey();
            }

            try
            {
                Common.Diagnostics.Log("Stopping the console host...");

                Common.Diagnostics.Log("Stopping the host...");

                host.Stop();
                
                Common.Diagnostics.Log("The host is stopped");
            }
            catch (Exception e)
            {
                Common.Diagnostics.Log(FormattableString.Invariant($"Unexpected error while stopping the host. {e.ToString()}"));
            }
            finally
            {
                Common.Diagnostics.Log("The console host is stopped");
                Console.ReadKey();
            }
        }

        private static string ReadConfiguratiion()
        {
            try
            {
                return File.ReadAllText("LocalForwarder.config");
            }
            catch (Exception e)
            {
                throw new ArgumentException(FormattableString.Invariant($"Could not read the configuration file. {e.ToString()}"), e);
            }
        }
    }
}