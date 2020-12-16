import config from "../../config";
import { GetToken } from "../../helpers/JwtService";
import { AvgRating } from "./Models/AvgRating";

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

    if (!response.ok) {
        throw Error("Failed to rate the post")
    }

    let result: AvgRating = await response.json()

    return result;

}