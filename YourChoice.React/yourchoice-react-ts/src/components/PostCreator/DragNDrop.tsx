import { createStyles, makeStyles, Theme } from '@material-ui/core/styles'
import React, { useRef, useState } from 'react'

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        dropZone: {
            width: '100%',
            height: '150px',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            alignContent: 'center',
            textAlign: 'center',
            cursor: 'pointer',
            color: 'grey',
            border: '4px dashed blue',
            borderRadius: '20px'
        },
        dropZoneOver: {
            borderStyle: 'solid'
        },
        defaultBox: {
            border: '1px solid black '
        }


    }),
);

interface DragAndDropProps {
    filesHandler: (_files: File[]) => void,
    text?: string
}

export const DragAndDrop = (props: DragAndDropProps) => {
    const classes = useStyles();
    const inputRef = useRef<HTMLInputElement>(null);
    const { filesHandler, text } = props

    let [dropZone, setDropZone] = useState(`${classes.dropZone}`);
    const dragOverHandler = (e: any) => {
        e.preventDefault()
        setDropZone(`${classes.dropZone} ${classes.dropZoneOver}`)

    }
    const dragLeaveHandler = (e: any) => {
        setDropZone(`${classes.dropZone}`)

    }


    const dropHandler = (e: any) => {
        e.preventDefault()
        setDropZone(`${classes.dropZone}`)
        let files = e.dataTransfer.files;
        filesHandler(files);

    }
    const clickHandler = (e: any) => {
        let input = inputRef.current;
        input?.click()
    }
    const inputChangeHandler = (e: any) => {
        let files = e.target.files;
        filesHandler(files);
    }
    return (

        <div className={dropZone}
            onDragOver={dragOverHandler}
            onDragLeave={dragLeaveHandler}
            onDrop={dropHandler}
            onDragEnd={dragLeaveHandler}
            onClick={clickHandler}>
            <h2>{text}</h2>
            <input type="file" name="photoFile" style={{ display: 'none' }} ref={inputRef} onChange={inputChangeHandler} />
        </div>

    )
}


export default DragAndDrop