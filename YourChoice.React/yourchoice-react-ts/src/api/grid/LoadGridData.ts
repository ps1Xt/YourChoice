import { GetToken } from '../../helpers/JwtService';
import { baseUrl } from '../baseUrl';
import { PageRequest } from './models/PageRequest';
import { PaginatedResult } from './models/PaginatedResult';
import { Row } from './models/Row';



export const LoadGridData = async (data: PageRequest) :Promise<PaginatedResult<Row>> => {
    let response = await fetch(baseUrl + `post/PaginatedSearch`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        },
        body: JSON.stringify(data)

    });
    if(!response.ok)
        throw Error("Data loading fail")

    return await response.json()
}