// import React, { useEffect, useRef, useState } from 'react'
// import { HubConnectionBuilder } from '@microsoft/signalr'
// import * as signalR from "@microsoft/signalr";
// import { GetToken } from '../../services/JwtService';
// export const Chat = () => {
//     const [connection, setConnetion] = useState<any>(null)
//     const [chat, setChat] = useState<any[]>([])
//     const latestChat = useRef<any>(null)

//     latestChat.current = chat;

//     useEffect(() => {
//         const newConnection = new HubConnectionBuilder()
//             .withUrl("http://localhost:5000/hubs/notify", {
//                 skipNegotiation: true,
//                 transport: signalR.HttpTransportType.WebSockets,
//                 accessTokenFactory: () => GetToken().split('Bearer ')[1]
//             })
//             .withAutomaticReconnect()
//             .build()

//         setConnetion(newConnection)
//     }, [])

//     useEffect(() => {
//         if (connection) {
//             connection.start()
//                 .then(result => {
//                     console.log('connected!')

//                     connection.on("NewPostNotify", message => {
//                         const updateChat = [...latestChat.current];
//                         updateChat.push(message)
//                         console.log(message)
//                         setChat(updateChat)
//                         console.log(chat)

//                     })
                    
//                 }).catch(e => console.log('connection failed', e))

//         }
//     }, [connection])

//     const Recive = () => {
//         try {
//             connection.invoke("NewPostNotify", "qwerty").catch((e)=>{console.log(e)})
//         }
//         catch {

//         }

//     }
//     return (<div>
//         <button onClick={Recive}>click</button>
//     </div>)
// }
export const x = 1