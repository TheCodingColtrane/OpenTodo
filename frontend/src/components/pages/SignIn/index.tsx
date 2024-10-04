import styles from './page.module.css'
import { User } from "../../../types"
import { useForm } from "react-hook-form"
import { authEndpoints, backend } from "../../../backend"
export default function SignIn () {
const {
    register,
    handleSubmit,
  } = useForm<User>()
  const onSubmit = handleSubmit(async (data) => {
    if(data.username === "" || data.passwordHash === ""){
        alert("campos vazios");
        return
    }
    const response = await backend.post(authEndpoints.base, data)
    if(response?.ok) window.location.href = "/board/choose"
  })   
   return (
     <div className={styles.loginContainer}>
        <div className={styles["login-form"]}>
            <h2>Login</h2>
            <form id={styles.loginForm} onSubmit={onSubmit}>
                <div className={styles["input-group"]}>
                    <label htmlFor="username">Username</label>
                    <input type="text" id="username" {...register("username")}  placeholder="Enter your username" required />
                </div>
                <div className={styles["input-group"]}>
                    <label htmlFor="password">Password</label>
                    <input type="password" id="password"  {...register("passwordHash")} placeholder="Enter your password" required />
                </div>
                <div className={styles["remember-me"]}>
                    <input type="checkbox" id={styles.rememberMe} name="rememberMe" />
                    <label htmlFor="rememberMe">Remember Me</label>
                </div>
                <button type="submit">Login</button>
                <div className={styles["forgot-password"]}>
                    <a href="#">Forgot Password?</a>
                </div>
            </form>
        </div>
    </div>
   )
    }