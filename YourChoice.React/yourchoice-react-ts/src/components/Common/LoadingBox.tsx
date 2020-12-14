import classes from '*.module.sass';
import { CircularProgress, createStyles, makeStyles } from '@material-ui/core';
import React from 'react'

const useStyles = makeStyles(() =>
    createStyles({
        loadingBox: {
            position:'absolute',
            left:'calc(50% - 20px);',
            top:'calc(50% - 20px);'
        }
    }),
);

export const LoadingBox = () => {

    const classes = useStyles();



    return (
        <div className={classes.loadingBox} > <CircularProgress /></div>
    )
}