using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using System;
using System.Diagnostics;
namespace ArchipelagoDeathCounter.Models;


public class Connector 
{
    private int port;
    private string game;
    private string slotname;
    private string ?password;
    private string adress = "archipelago.gg";
    public event Action<string>? DeathLinkReceived;


    /**
        @param port Portnummer des Archipelago-Servers
        @param game Name des Spiels, das in Archipelago gespielt wird
        @param slotname Name des Slots, der in Archipelago verwendet wird
        @param password Passwort für den Raum, falls erforderlich
    */
    public Connector(int port, string game, string slotname, string password)
    {

        this.port = port;
        this.game = game;
        this.slotname = slotname;
        this.password = password;
    }
    public Connector(string adress,int port, string game, string slotname, string password)
    {

        this.port = port;
        this.game = game;
        this.slotname = slotname;
        this.password = password;
        this.adress = adress;
    }


    public bool Connect()
    {
        try
        {
            var session = ArchipelagoSessionFactory.CreateSession(adress, port);
            var deathLinkService = session.CreateDeathLinkService();

            deathLinkService.OnDeathLinkReceived += (deathLinkObject) =>
            {
                DeathLinkReceived?.Invoke(deathLinkObject.Source);
            };

            var result = session.TryConnectAndLogin(
                            game: game,
                            name: slotname,
                            password: password,
                            itemsHandlingFlags: ItemsHandlingFlags.NoItems,
                            tags: new[] { "DeathLink" }
                            );

            if (!result.Successful)
            {
                Debug.WriteLine("Connection error: " + string.Join(", ", ((LoginFailure)result).Errors));
                return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }


    public void Disconnect()
    {
        // Does it need 
    }

    
}
