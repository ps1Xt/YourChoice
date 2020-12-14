import config from "../../config";
import { GetToken } from "../../services/JwtService";
import { NewMessages } from "./Models/NewMessages";

const baseUrl = config.API_URL;

export const getCountOfNewMessages = async (): Promise<NewMessages>=> {
    
    let response = await fetch(baseUrl + "notification/count", {
        method: 'get',
        headers: {
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        }

    });

    if(!response.ok)
        throw Error("Unauthorized")

    let json : NewMessages = await response.json();

    return json;

}