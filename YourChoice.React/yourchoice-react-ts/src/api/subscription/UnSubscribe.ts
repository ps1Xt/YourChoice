import { GetToken } from "../../helpers/JwtService";
import { baseUrl } from "../baseUrl";

export const unSubscribe = async (toWhomUserName: string) => {
    let response = await fetch(baseUrl + `subscription`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        },
        body: JSON.stringify({
            userName: toWhomUserName
        })
        
    });

    if(!response.ok){
        throw Error("Failed to unsubscribe")
    }
    
    let result = await response.json();

    return result;

}