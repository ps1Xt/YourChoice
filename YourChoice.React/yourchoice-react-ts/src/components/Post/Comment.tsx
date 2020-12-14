import { createStyles, Grid, makeStyles, Theme, Typography } from '@material-ui/core'
import React from 'react'
import { PostComment } from '../../api/post/Models/PostComment';
const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        userName: {
            fontWeight: 'bold',
            marginRight: '10px',
        },
        date: {
            color: 'grey'
        },
        comment: {
            fontWeight: 100
        }
    }),
);


export const Comment = (props: PostComment) => {
    let classes = useStyles();
    return (
        <div>
            <Grid container>
                <Grid item>
                    <Typography variant="h6" className={classes.userName}>{props.userName}</Typography>
                </Grid>
                <Grid item>
                    <Typography variant="h6" className={classes.date}>{props.date}</Typography>
                </Grid>
                <Grid item xs={12}>
                    <Typography variant="h6" className={classes.comment}>{props.text}</Typography>
                </Grid>
            </Grid>
        </div>
    )
}
