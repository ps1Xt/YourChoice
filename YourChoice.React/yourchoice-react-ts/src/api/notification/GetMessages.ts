import { GetToken } from "../../services/JwtService";
import { baseUrl } from "../baseUrl";
import { Message } from "./Models/Message";


export const getMessages = async (): Promise<Message[]>=> {

    let response = await fetch(baseUrl + "notification", {
        method: 'get',
        headers: {
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        }

    });

    return await response.json();


}


