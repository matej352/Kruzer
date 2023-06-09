import React from "react";
import {
  Button,
  Form,
  Input,
  Space,
  Typography,
  message,
  notification,
} from "antd";
import SubmitButton from "../buttons/submitButton";
const { Title } = Typography;
import { api } from "@/src/core/api";

export default function Passenger({ passenger, setRefetch }) {
  const [messageApi, contextHolder] = message.useMessage();

  const { ime, prezime, nadimak, email } = passenger;

  function handleDeletionNotification() {
    notification.open({
      message:
        "Brisanje putnika može rezultirati brisanjem pripadajućih rezervacija! Jeste li sigurni?",
      btn: (
        <Button type="primary" size="small" onClick={handlePassengerDelete}>
          Potvrdi
        </Button>
      ),
      duration: null,
    });
  }

  async function handlePassengerDelete() {
    const response = api
      .delete("/api/Putnik/" + passenger.id)
      .then((res) => {
        notification.open({
          message: "Putnik obrisan!",
        });
        setRefetch((prev) => !prev);
      })
      .catch((error) => {
        notification.open({
          message: "Dogodila se pogreška, pokušajte ponovno!",
        });
      });

    /*if (response.status == 204) {
      notification.open({
        message: "Putnik obrisan!",
      });
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }*/
  }

  async function onFinish() {
    await form.validateFields();
    const data = form.getFieldsValue();
    const response = api
      .put("/api/Putnik/" + passenger.nadimak, data)
      .then((res) => {
        notification.open({
          message: "Podaci putnika ažurirani!",
        });
        setRefetch((prev) => !prev);
      })
      .catch((error) => {
        notification.open({
          message: "Dogodila se pogreška, pokušajte ponovno!",
        });
      });
    form.resetFields();
    /*if (response.status == 204) {
      notification.open({
        message: "Podaci putnika ažurirani!",
      });
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }
    */
  }

  const [form] = Form.useForm();

  return (
    <div>
      <div className="flex justify-between items-center">
        <Title level={3}>Putnik</Title>
        <Button type="primary" danger onClick={handleDeletionNotification}>
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
