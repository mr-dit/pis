import React from "react";
import { Button, Form, Input } from "antd";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import "./AuthPage.css";
import { saveToLocalStorage } from "../../helpers";

const { REACT_APP_API_URL } = process.env;



const AuthPage = () => {
  const navigate = useNavigate();

  const onFinish = async (values) => {
    try {
      const res = await axios.post(
        `${REACT_APP_API_URL}/AuthControllercs/login?login=${values.login}&password=${values.password}`
      );
      navigate("/Animal");
      saveToLocalStorage(res);
    } catch (error) {
      alert("Неверный логин и пароль");
    }
  };
  

  return (
    <div className="pt-5 auth-form">
      <h1 style={{ textAlign: "center" }}>Вход в систему</h1>
      <Form
        name="basic"
        labelCol={{
          span: 8,
        }}
        wrapperCol={{
          span: 16,
        }}
        style={{
          maxWidth: 600,
        }}
        initialValues={{
          remember: true,
        }}
        onFinish={onFinish}
        autoComplete="off"
      >
        <Form.Item
          label="Логин"
          name="login"
          rules={[
            {
              required: true,
              message: "Укажите логин!",
            },
          ]}
        >
          <Input size="large" />
        </Form.Item>

        <Form.Item
          label="Пароль"
          name="password"
          rules={[
            {
              required: true,
              message: "Укажите пароль!",
            },
          ]}
        >
          <Input.Password size="large" />
        </Form.Item>

        <Form.Item
          wrapperCol={{
            offset: 8,
            span: 16,
          }}
        >
          <Button type="primary" htmlType="submit" size="large">
            Вход
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};

export default AuthPage;
