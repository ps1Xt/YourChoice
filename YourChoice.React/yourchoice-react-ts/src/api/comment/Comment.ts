import { GetToken } from "../../helpers/JwtService";
import { baseUrl } from "../baseUrl";


export const comment = async (postId: number, text: string) : Promise<string> => {

    let response = await fetch(baseUrl + "comment", {
        method: 'post',
        headers: {
            "Accept": "application/json",
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        },
        body: JSON.stringify({
            PostId: postId,
            Text: text
        })
    });

    if(!response.ok){
        throw Error("Failed to comment")
    }
    
    let result = await <string>response.headers.get('Location')

    return result;

}

