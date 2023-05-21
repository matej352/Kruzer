import { Select, Modal, Form, Input, notification } from "antd";
import { useState, useEffect } from "react";
import { api } from "@/src/core/api";

function PassengerModal({ visible, setVisible }) {
  const handleCancel = () => {
    setVisible(false);
  };

  const handleGengerChange = (option) => {
    form.setFieldValue("spol", option);
  };

  async function handleOk() {
    const data = form.getFieldsValue();
    if (!data.spol) {
      data.spol = "M";
    }
    const response = api
      .post("/api/Putnik/Create", data)
      .then((response1) => {
        return response1.json();
      })
      .then((res) => {
        notification.open({
          message: "Putnik kreiran!",
        });
      })
      .catch((error) => {
        notification.open({
          message: "Dogodila se pogreška, pokušajte ponovno!",
        });
      });

    /*if (response.status == 204) {
      notification.open({
        message: "Putnik kreiran!",
      });
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }*/
    setVisible(false);
  }

  const [form] = Form.useForm();

  return (
    <>
      <Modal
        title="Novi putnik"
        open={visible}
        onOk={handleOk}
        onCancel={handleCancel}
        okText="Pošalji"
        cancelText="Natrag"
      >
        <Form
          form={form}
          layout="vertical"
          autoComplete="off"
          name="validateOnly"
        >
          <Form.Item
            name="ime"
            label="Ime"
            rules={[
              {
                required: true,
              },
            ]}
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
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="lozinka"
            label="Lozinka"
            rules={[
              {
                required: true,
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="spol"
            label="Spol"
            rules={[
              {
                required: true,
              },
            ]}
          >
            <Select
              defaultValue="M"
              onChange={handleGengerChange}
              options={[
                {
                  value: "M",
                  label: "M",
                },
                {
                  value: "Ž",
                  label: "Ž",
                },
              ]}
            />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
}
export default PassengerModal;
