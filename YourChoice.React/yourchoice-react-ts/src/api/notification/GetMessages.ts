import { GetToken } from "../../helpers/JwtService";
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

    if(!response.ok)
        throw Error("Failed to load messages")

    return await response.json();


}


