import { action, payload } from 'ts-action';

export const newMessage = action('NEW_MESSAGE');

export const readMessages = action('READ_MESSAGES');

export const setNewMessages = action('SET_NEW_MESSAGES', payload<{ number: number }>())