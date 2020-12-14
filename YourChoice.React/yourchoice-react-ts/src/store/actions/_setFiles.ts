import { action, payload } from 'ts-action';

export const update = action('UPDATE', payload<{ update: boolean }>());

