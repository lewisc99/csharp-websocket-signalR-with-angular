import { Injectable } from "@angular/core";
import { HubConnection } from "@aspnet/signalr";
import { BehaviorSubject, Subject } from "rxjs";
import { Message } from "./message";
import * as signalR from '@aspnet/signalr';

@Injectable({
    providedIn:"root"
})


export class NotificationService
{
    constructor() {
        this.initConnection();
    }

    private hubConnection!:HubConnection;
    public messages: Subject<Message> = new BehaviorSubject<Message>({});



    private initConnection():void 
    {
        this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:5001/" + "notify")
        .build();

        this.hubConnection.start().then(() => console.log("Hub connection started")).catch(() => console.log("error"));
    }

    receiveMessages():void {
        this.hubConnection.on('ReceiveMessage', (data) =>
        {
            console.log("Received");
            console.log("Data: " + JSON.stringify(data));

            var newMessage:Message = new Message();
            newMessage.Name = data.user;
            newMessage.Message = data.messageSent;
            this.messages.next(newMessage);
        });
    }

    notificationMessage():void 
    {
        this.hubConnection.on('broadcastNotification', (data) =>
        {
            console.log(data);
          
            var newMessage:Message = new Message();
            newMessage.Name = data.name;
            newMessage.Message = data.message;
            console.log(newMessage);
            this.messages.next(newMessage);
        });
    }

     sendMessage(message:Message) :void 
    {
            this.hubConnection.invoke("SendMessage",message);
    }
    
}