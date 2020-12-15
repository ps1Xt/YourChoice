import { createStyles, makeStyles, Typography } from '@material-ui/core';
import React from 'react';

interface IErrorBox {
    message?: string,
    variant?: "inherit" | "h1" | "h2" | "h3" | "h4" | "h5" | "h6" | "subtitle1" |
    "subtitle2" | "body1" | "body2" | "caption" | "button" | "overline" | "srOnly" | undefined,
    color?: "inherit" | "primary" | "secondary" | "initial" | "textPrimary" | "textSecondary" | "error" | undefined
}
const useStyles = makeStyles(() =>
    createStyles({
        loadingBox: {
            position: 'absolute',
            top: 'calc(50%);',
            textAlign: 'center',
            left: '50%',
            transform: 'translate(-50%, -50%)'
        }
    }),
);

export const ErrorBox = (props: IErrorBox) => {

    const { message, variant, color } = props;
    const classes = useStyles();

    return (
        <Typography className={classes.loadingBox} color={color} variant={variant}>{message}</Typography>
    )

}