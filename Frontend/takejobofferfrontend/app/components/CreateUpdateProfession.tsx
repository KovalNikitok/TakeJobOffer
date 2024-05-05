import { Input } from "antd";
import { ProfessionWithSlugRequest } from "../services/professions";
import { useEffect, useState } from "react";
import TextArea from "antd/es/input/TextArea";
import Modal from "antd/es/modal/Modal";
import { Mode } from "./Mode"

interface Props {
  mode: Mode;
  values: ProfessionWithSlug;
  isModalOpen: boolean;
  handleCancel: () => void;
  handleCreate: (request: ProfessionWithSlugRequest) => void;
  handleUpdate: (id: string, request: ProfessionWithSlugRequest) => void;
}

export const CreateUpdateProfession = ({
  mode,
  values,
  isModalOpen,
  handleCancel,
  handleCreate,
  handleUpdate,
}: Props) => {
  const [name, setName] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [slug, setSlug] = useState<string>("");

  useEffect(() => {
    setName(values.name);
    setDescription(values.description);
    setSlug(values.slug);
  }, [values]);

  const handleOnOk = async () => {
    const professionWithSlugRequest = { name, description, slug };

    mode == Mode.Create
      ? handleCreate(professionWithSlugRequest)
      : handleUpdate(values.id, professionWithSlugRequest);
  };

  return (
    <Modal
      title={
        mode === Mode.Create ? "Добавить профессию" : "Редактировать профессию"
      }
      open={isModalOpen}
      onOk={handleOnOk}
      onCancel={handleCancel}
      cancelText={"Отмена"}
    >
      <div className="profession__modal">
        <Input
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Название"
        />
        <TextArea
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          autoSize={{ minRows: 3, maxRows: 3 }}
          placeholder="Описание"
        />
        <Input
          value={slug}
          onChange={(e) => setSlug(e.target.value)}
          placeholder="Url для доступа  (* не обязательное поле)"
        />
      </div>
    </Modal>
  );
};
