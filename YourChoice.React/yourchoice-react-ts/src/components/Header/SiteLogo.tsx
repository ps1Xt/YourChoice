import React from 'react';
import {Box, Typography } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';


const usesStyles = makeStyles(() => ({
    left: {
        color: 'white'
    },
    right: {
        color: 'red'
    }
}));

export function SiteLogo() {

    const classes = usesStyles();

    return (
        <Box display="flex">

            <Typography variant="h4" className={classes.left} >Your</Typography>
            <Typography variant="h4" className={classes.right} >Choice</Typography>

        </Box>

    );
}
