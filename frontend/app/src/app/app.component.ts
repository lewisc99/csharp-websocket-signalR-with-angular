import { Component, OnInit } from '@angular/core';
import { NotificationService } from './notification.service';
import { Stomp } from '@stomp/stompjs';
import SockJS from  "sockjs-client"
import { Message } from './message';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit{
  
  title = "app";
  newMessage:Message[] = [];

  constructor(private notificationService:NotificationService){}


  ngOnInit(): void {

    this.notificationService.notificationMessage();
   this.receivedMessage();
 
  }

  sendMessage( name:string,  message:string)
    {
     var messageObject:Message = new Message();

     messageObject.Message = message;
     messageObject.Name = name;
      
     this.notificationService.sendMessage(messageObject);
    }

  public  receivedMessage():void
    {
     
       this.notificationService.messages.pipe().subscribe(
        async (data:Message) =>
        {
          console.log("data? " + data);
          this.newMessage.push(data);
        }
      )
    }

  


}
