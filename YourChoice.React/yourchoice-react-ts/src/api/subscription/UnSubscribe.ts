import { GetToken } from "../../services/JwtService";
import { baseUrl } from "../baseUrl";

export const unSubscribe = (toWhomUserName: string) => {
    return fetch(baseUrl + `subscription`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        },
        body: JSON.stringify({
            userName: toWhomUserName
        })
        
    });
}