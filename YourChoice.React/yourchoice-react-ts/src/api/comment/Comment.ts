import { GetToken } from "../../services/JwtService";
import { baseUrl } from "../baseUrl";


export const comment = (postId: number, text: string) : Promise<string> => {

    return fetch(baseUrl + "comment", {
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
    }).then(response => <string>response.headers.get('Location'));

}

