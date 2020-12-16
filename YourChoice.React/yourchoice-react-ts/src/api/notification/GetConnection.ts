import * as signalR from "@microsoft/signalr";
import { GetToken } from '../../helpers/JwtService';
import { HubConnectionBuilder } from '@microsoft/signalr'

export const getConnection = () => {
    const connection = new HubConnectionBuilder()
        .withUrl("http://localhost:5000/hubs/notify", {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
            accessTokenFactory: () => GetToken().split('Bearer ')[1]
        })
        .withAutomaticReconnect()
        .build();


    return connection;
}

export default getConnection