import { GetToken } from "../../services/JwtService";
import { baseUrl } from "../baseUrl";



export const subscribe = (toWhomUserName: string) => {
    return fetch(baseUrl + `subscription`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        },
        body: JSON.stringify({
            userName: toWhomUserName
        })
        
    });
}

