import { on, reducer } from "ts-action";
import { update } from "../actions/_setFiles";

export interface setfiles {
    files: File[],
    filesSrc: any[],
    update: boolean
}

const initialState: setfiles = {
    files: new Array<File>(),
    filesSrc: new Array<any>(),
    update: false
};

export default reducer(
    initialState,
    on(update, (state, {payload})=>({
        ...state,
        update: true
    }))
    
    
)

