import { Button, Grid, OutlinedInput } from '@material-ui/core'
import React, { useState } from 'react'


interface ILeaveComment {
    xs?: boolean | "auto" | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | undefined
    xl?: boolean | "auto" | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | undefined
    sm?: boolean | "auto" | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | undefined
    md?: boolean | "auto" | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | undefined
    lg?: boolean | "auto" | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | undefined
    rowsMax?: string | number | undefined
    rows?: string | number | undefined
    buttonColor?: "default" | "inherit" | "primary" | "secondary" | undefined
    placeholder?: string | undefined
    name?: string | undefined
    inputRef?: ((instance: any) => void) | React.RefObject<any> | null | undefined
}

export const LeaveComment = (props: ILeaveComment) => {
    let { xs, xl, sm, md, lg, rowsMax, rows, buttonColor, placeholder, name, inputRef } = props;

    const [value, setValue] = useState("");

    const onChange = (str : string) =>{
        setValue(str);
    }
    const onCLick = () =>{
        setTimeout(()=>{
            setValue("");
        },1)
    }

    return (
        <Grid container justify='flex-start' spacing={1}>
            <Grid item xs={xs} xl={xl} sm={sm} md={md} lg={lg}>
                <OutlinedInput
                    id="outlined-multiline-static"
                    multiline
                    rows={rows}
                    rowsMax={rowsMax}
                    placeholder={placeholder}
                    fullWidth
                    name={name}
                    inputRef={inputRef}
                    value={value}
                    onChange={(e)=>onChange(e.target.value)}
                />

            </Grid>
            <Grid item xs={xs} xl={xl} sm={sm} md={md} lg={lg}>
                <div style={{ display: 'flex', justifyContent: "flex-end", }}>
                    <Button
                        variant="contained"
                        color={buttonColor}
                        type="submit"
                        onClick={onCLick}
                    >
                        COMMENT
                    </Button>
                </div>
            </Grid>
        </Grid>
    )
}