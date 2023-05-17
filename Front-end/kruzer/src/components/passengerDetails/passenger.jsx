import React from "react";
import { Button, Form, Input, Typography, message, notification } from "antd";
import SubmitButton from "../buttons/submitButton";
const { Title } = Typography;
import { api } from "@/src/core/api";

export default function Passenger({ passenger }) {
  const [messageApi, contextHolder] = message.useMessage();

  const { ime, prezime, nadimak, email } = passenger;

  const [form] = Form.useForm();

  async function handlePassengerDelete() {
    const response = await api.delete("/api/Putnik/" + passenger.id);
    if (response.status == 204) {
      notification.open({
        message: "Putnik obrisan!",
      });
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }
  }

  async function onFinish() {
    const data = form.getFieldsValue();
    const response = await api.put("/api/Putnik/" + data.nadimak, data);

    if (response.status == 204) {
      notification.open({
        message: "Podaci putnika ažurirani!",
      });
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }
  }

  return (
    <div>
      <div className="flex justify-between items-center">
        <Title level={3}>Putnik</Title>
        <Button type="primary" danger onClick={handlePassengerDelete}>
          Obriši putnika
        </Button>
      </div>
      <Form
        form={form}
        name="validateOnly"
        layout="vertical"
        autoComplete="off"
        onFinish={onFinish}
      >
        <Form.Item
          name="ime"
          label="Ime"
          rules={[
            {
              required: true,
            },
          ]}
          initialValue={ime}
        >
          <Input />
        </Form.Item>
        <Form.Item
          name="prezime"
          label="Prezime"
          rules={[
            {
              required: true,
            },
          ]}
          initialValue={prezime}
        >
          <Input />
        </Form.Item>
        <Form.Item
          name="nadimak"
          label="Nadimak"
          rules={[
            {
              required: true,
            },
          ]}
          initialValue={nadimak}
        >
          <Input />
        </Form.Item>
        <Form.Item
          name="email"
          label="Email"
          rules={[
            {
              required: true,
            },
          ]}
          initialValue={email}
        >
          <Input />
        </Form.Item>
        <Form.Item>
          <SubmitButton form={form} />
        </Form.Item>
      </Form>
    </div>
  );
}
