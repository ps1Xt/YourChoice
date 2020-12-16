import { GetToken } from "../../helpers/JwtService";
import { baseUrl } from "../baseUrl";



export const subscribe = async (toWhomUserName: string) => {
    let response = await fetch(baseUrl + `subscription`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        },
        body: JSON.stringify({
            userName: toWhomUserName
        })
        
    });

    if(!response.ok){
        throw Error("Failed to subscribe")
    }
    
    let result = await response.json();

    return result;
}

