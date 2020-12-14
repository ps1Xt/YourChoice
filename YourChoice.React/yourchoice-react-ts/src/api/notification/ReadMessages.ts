import { GetToken } from "../../services/JwtService";
import { baseUrl } from "../baseUrl";


export const readMessages = async (): Promise<boolean>=> {

    let response = await fetch(baseUrl + "notification", {
        method: 'PATCH',
        headers: {
            "Authorization": GetToken(),
        }

    });

    let json =  await response.json();

    let result = json.result;

    return result;
}