import config from "../../config";
import { GetToken } from "../../services/JwtService";

const baseUrl = config.API_URL;



export const ratePost = async (postId: number, value: number) => {
    let response = await fetch(baseUrl + 'Rating', {
        method: 'put',
        headers: {
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        },
        body: JSON.stringify({
            Id: postId,
            Value: value
        })

    });
    return response;

}