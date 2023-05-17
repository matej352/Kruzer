import { Select, Modal, Form, Input, notification } from "antd";
import { useState, useEffect } from "react";
import { api } from "@/src/core/api";

function CruisingModal({ visible, setVisible, reservation = {}, setRefetch }) {
  const { id, brojputnika } = reservation;
  const handleCancel = () => {
    setVisible(false);
  };

  async function handleOk() {
    setVisible(false);
    const data = form.getFieldsValue();
    console.log("data ", data);
    const response = await api.put("/api/Rezervacija/" + id, data);

    if (response.status == 204) {
      notification.open({
        message: "Rezervacija ažurirana!",
      });
      setRefetch((prev) => !prev);
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }
  }

  const [form] = Form.useForm();

  return (
    <>
      <Modal
        title="Uredi rezervaciju"
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
            name="brojputnika"
            label="Broj putnika"
            rules={[
              {
                required: true,
              },
            ]}
            initialValue={brojputnika}
          >
            <Input />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
}
export default CruisingModal;
