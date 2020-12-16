import { GetToken } from "../../helpers/JwtService";
import { baseUrl } from "../baseUrl";


export const readMessages = async (): Promise<boolean>=> {

    let response = await fetch(baseUrl + "notification", {
        method: 'PATCH',
        headers: {
            "Authorization": GetToken(),
        }

    });

    if(!response.ok){
        throw Error("Failed to read messages")
    }

    let result =  await response.json();

    return result;
}