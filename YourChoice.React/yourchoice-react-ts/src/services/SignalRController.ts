import React, { useEffect, useRef, useState } from 'react'
import { HubConnectionBuilder } from '@microsoft/signalr'
import * as signalR from "@microsoft/signalr";
import { GetToken } from './JwtService';
import { AsyncAction } from '../store/actions/_auth'
import { newMessage } from '../store/actions/_notification';
import { useDispatch } from 'react-redux';


export const newPostNotifyServer = (connection: any) => {
    connection.invoke("NewPostNotify");
}


// export class SignalRController {
//     public connection

//     constructor() {
//         this.
//     }
//     favoritesEvent = () => {
//         this.connection.on('FavoritesEvent', () => {
//             {
//                 console.log("good")
//                 this.test()
//             }
//         })
//     }
//     favoritesNotify = () =>{
//         this.connection.invoke("FavoritesEvent");
//     }

//     test = () : AsyncAction => async (dispatch) =>{

//         dispatch(newMessage())

//     }
// }