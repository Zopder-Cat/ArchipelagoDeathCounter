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

   
    public bool Connect()
    {
        var session = ArchipelagoSessionFactory.CreateSession("archipelago.gg", port);
        var deathLinkService = session.CreateDeathLinkService();
        
        //deathLinkService.EnableDeathLink();

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


    public void Disconnect()
    {
        // Does it need 
    }

    
}
