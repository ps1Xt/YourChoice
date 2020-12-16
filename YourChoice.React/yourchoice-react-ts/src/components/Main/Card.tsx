import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Collapse from '@material-ui/core/Collapse';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { DateToString } from '../../helpers/DateService';
import { PostCard } from '../../api/post/Models/PostCard';
import { useHistory } from 'react-router-dom';
const useStyles = makeStyles((theme) => ({
    root: {
        maxWidth: 250,
        cursor: 'pointer',
        minWidth: 225,
        margin: '0 auto'
    },
    media: {
        height: 0,
        paddingTop: '56.25%', // 16:9
    },
    expand: {
        transform: 'rotate(0deg)',
        marginLeft: 'auto',
        transition: theme.transitions.create('transform', {
            duration: theme.transitions.duration.shortest,
        }),
        outline: 'none'

    },
    expandOpen: {
        transform: 'rotate(180deg)',
        outline: 'none'

    },
    cardHeaderRoot: {
        overflow: "hidden"
    },
    cardHeaderContent: {
        overflow: "hidden"
    },


}));






export default function RecipeReviewCard(props: PostCard) {
    const classes = useStyles();
    const [expanded, setExpanded] = React.useState(false);
    const history = useHistory();
    const data = props;
    const handleExpandClick = () => {
        setExpanded(!expanded);
    };
    const onPostClick = () =>{
        history.push("post/" + data.id)
    }

    return (
        <Card className={classes.root} elevation={4}>
            <CardHeader
                classes={{
                    root: classes.cardHeaderRoot,
                    content: classes.cardHeaderContent
                }}
                title={
                    <Typography noWrap gutterBottom variant="h6" component="h4">
                        {data.title}
                    </Typography>
                }
                subheader={DateToString(new Date(data.date), 'yyyy-MM-dd')}
            />
            <CardMedia
                className={classes.media}
                image={data.logo}
                onClick={onPostClick}
            />

            <CardActions disableSpacing>
                <Typography>
                    Rating: <span style={{ fontWeight: 'bold' }}>{data.avgRating}/10</span>
                </Typography>
                <IconButton
                    className={clsx(classes.expand, {
                        [classes.expandOpen]: expanded,
                    })}
                    onClick={handleExpandClick}
                    aria-expanded={expanded}
                    aria-label="show more"
                >
                    <ExpandMoreIcon />
                </IconButton>

            </CardActions>
            <Collapse in={expanded} timeout="auto" unmountOnExit >
                <CardContent>
                    <Typography paragraph>
                        Author: <span style={{ fontWeight: 'bold' }}>{data.userName}</span>
                    </Typography>
                    <Typography paragraph>
                        {data.description}
                    </Typography>
                </CardContent>
            </Collapse>
        </Card>

    );
}