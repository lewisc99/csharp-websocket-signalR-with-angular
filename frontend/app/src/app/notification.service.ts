import { Injectable } from "@angular/core";
import { HubConnection } from "@aspnet/signalr";
import { BehaviorSubject } from "rxjs";
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
    private messages: Message[] = [];
    public message:any = new BehaviorSubject<Message[]>([]);


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
            this.messages.push(newMessage);
            this.message.next(this.messages);
        });
    }

    notificationMessage():void 
    {
        this.hubConnection.on('broadcastNotification', (data) =>
        {
            console.log(data);
            console.log("HEY, Employee has been notified");
        });
    }

     sendMessage(message:Message) :void 
    {
            this.hubConnection.invoke("SendMessage",message);
     
    }

    
}