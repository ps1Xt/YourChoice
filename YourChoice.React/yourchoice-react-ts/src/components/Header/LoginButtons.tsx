import { Button, createStyles, Grid, makeStyles } from '@material-ui/core'
import React from 'react'
import { Link } from 'react-router-dom';

const useStyles = makeStyles(() =>
    createStyles({

        noOutline: {
            outline: 'none',
            textDecoration: 'none'
        },
        link:{
            textDecoration: 'none'
        }
    }),
);

export const LoginButtons = () => {

    const classes = useStyles();

    return (
        <Grid container>

            <Grid item >
                <Link className={classes.link} href="" to="/SignIn">
                    <Button variant="outlined" color="primary" className={classes.noOutline}  >Sign In</Button>

                </Link>
            </Grid>
            <Grid item >
                <Link className={classes.link} href="" to="/SignUp">
                    <Button color="secondary" variant="outlined" className={classes.noOutline}>Sign Up</Button>
                </Link>
            </Grid>
        </Grid>

    )
}