import { User } from "../../../types"
import { useForm } from "react-hook-form"
import { backend, userEndpoints } from "../../../backend"
import styles from './page.module.css'
import { convertDateLocaleBRToDateOnly, isDate, maskDate, showMessage } from "../../../utils"
import { useState } from "react"
export default function SignUp() {
    const {
        register,
        handleSubmit,
    } = useForm<User>()

    const [isUsernameTaken, setIsUsernameTaken] = useState(false)
    const onSubmit = handleSubmit(async (data) => {
        if(isUsernameTaken) return
        if(data.firstName === "" || data.lastName === "" || isDate(data.dob2 ?? "") || data.username === "" || data.passwordHash === ""){
            showMessage('Campos inválidos.')
            return  

        }
        data.dob2 = convertDateLocaleBRToDateOnly(data.dob2!)
        const response = await backend.post(userEndpoints.base, { firstName: data.firstName, lastName: data.lastName, username: data.username, passwordHash: data.passwordHash, dob: data.dob2 })
        if (response?.ok) window.location.href = "/signin"
    })

    const checkUsernameAvailability = async (username: HTMLInputElement) => {
        const result = await backend.get<{ isTaken: boolean }>(userEndpoints.getUsernameAvailability(username.value))
        if (result?.isTaken) {
            showMessage('Este usuário não está disponível. Forneça outro.')
            setIsUsernameTaken(true)
        }
        setIsUsernameTaken(false)
    }



    return (
        <div className={styles["signup-container"]}>
            <form className={styles["signup-form"]} onSubmit={onSubmit}>
                <h2>Cadastro</h2>
                <div className={styles["input-group"]}>
                    <label>Primeiro Nome</label>
                    <input type="text" {...register("firstName")} required />
                </div>
                <div className={styles["input-group"]}>
                    <label>Último Nome</label>
                    <input type="text" {...register("lastName")} required />
                </div>
                <div className={styles["input-group"]}>
                    <label>Data de Nascimento</label>
                    <input type="text" {...register("dob2")} onKeyDown={(e) => maskDate(e, e.target as HTMLInputElement)} minLength={10} maxLength={10} required />
                </div>
                <div className={styles["input-group"]}>
                    <label>Usuário</label>
                    <input type="text" {...register("username")} onKeyDown={(e) => checkUsernameAvailability(e.target as HTMLInputElement)} required/>
                </div>
                <div className={styles["input-group"]}>
                    <label>Senha</label>
                    <input type="password"  {...register("passwordHash")} required/>
                </div>
                <button type="submit" value={"Cadastrar"}>Cadastrar</button>
            </form>
        </div>
    )
}